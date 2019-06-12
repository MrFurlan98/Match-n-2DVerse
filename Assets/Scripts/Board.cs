﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public enum GameState
{
    STANDBY,
    RUNNING,
    WAITING,
    onMatch,
    finishMatch
}

public enum E_Direction
{
    LEFT,
    RIGHT,
    UP,
    DOWN,
    STAY
}

public enum BoardScenario
{
    STANDBY,
    APOCALIPTICO,
    GREGO
}

public enum BoardType
{
    STANDBY,
    RESGATE,
    DESATIVAR_BOMBA,
    UM_DOS_DOZE_TRABALHOS,
    SOB_O_OLHAR_DA_GORGONA
}
public class Board : MonoBehaviour {
   
    private GameState m_CurrentState = GameState.STANDBY;
    private BoardScenario m_CurrentScenario = BoardScenario.STANDBY;
    private BoardType m_CurrentType = BoardType.STANDBY;


    private BoardIcon[,] m_Icons;

    private Level level;

    private System.Action m_TriggerMatch;
    // create a queue to do actions before the next match 
    [SerializeField]
    private List<System.Action> m_Actions = new List<System.Action>();

    [SerializeField]
    private List<System.Action> m_BoardModifies = new List<System.Action>();

    private Vector2 m_InitPosition;

    [Header("Config Board Game")]

    [SerializeField]
    private Sprite shadow;

    [SerializeField]
    private Sprite stone;

    [SerializeField]
    private int m_Heigth;

    [SerializeField]
    private int m_Width;

    [Range(0, 3)]
    [SerializeField]
    private float m_RefillDelay;

    [Range(0, 3)]
    [SerializeField]
    private float m_DropDelay;

    [Range(0, 3)]
    [SerializeField]
    private float m_SwapDelay;

    [Range(0, 3)]
    [SerializeField]
    private float m_SpecialIconDelay;

    [SerializeField]
    private int m_OffSet = 0;

    [SerializeField]
    private float m_TimeToStart;


    public IEnumerator StartDalay()
    {
        m_CurrentState = GameState.RUNNING;
        yield return new WaitForSeconds(m_TimeToStart);
        m_CurrentState = GameState.STANDBY;
    }

    private void Start()
    {
        m_CurrentState = GameState.RUNNING;
        m_Icons = new BoardIcon[Width, Heigth];
    }

    private void Update()
    {
       
        if (m_CurrentState == GameState.RUNNING)
            return;

        if (OnMouseDown() && !IsOverUI())
        {
            m_InitPosition = MousePosition();
        }
        if (OnMouseUp() && !IsOverUI())
        {
            Vector2 tEndPosi = MousePosition();

            Vector2Int tIconIndex = FindIcon(m_InitPosition);

            if(tIconIndex.x > -1)
            {
                // swipe 
                E_Direction tDirection = GetDirection(m_InitPosition, tEndPosi);

                StartCoroutine(BoardRunning(tIconIndex, tDirection));

            //    Debug.Log("Mouse Clicked At Icon (" + tIconIndex +")" + " To direction "+ tDirection.ToString());
            }
          
        }

    }
    public BoardIcon[,] GetBoardIcons()
    {
        return m_Icons;
    }
    public void ClearBoard()
    {
        
  

        for (int i = 0; i < m_Icons.GetLength(0); i++)
        {
            for (int j = 0; j < m_Icons.GetLength(1); j++)
            {
                if (m_Icons[i, j] != null)
                    Destroy(m_Icons[i, j].gameObject);
                m_Icons[i, j] = null;

            }
        }
    }
    public void InitBoard(int levelSelect)
    {
        m_CurrentState = GameState.RUNNING;
        StopAllCoroutines();
        //StartCoroutine(StartDalay());

        level = BoardManager.Instance.Levels[levelSelect];

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Heigth; j++)
            {
                if(level.Model[i,j]==1)
                {
                    BoardIcon tIconItem = IconManager.Instance.GenerateRandomIcon(i, j,level.Scenario);

                    tIconItem.colunm = j + OffSet;
                    tIconItem.row = i;
                    m_Icons[i, j] = tIconItem;
                }
                else
                {

                    BoardIcon tIconIndestructItem = IconManager.Instance.GenerateIndestructableIcon(i, j);
                    tIconIndestructItem.colunm = j + OffSet;
                    tIconIndestructItem.row = i;
                    m_Icons[i, j] = tIconIndestructItem;
                }
            }
        }
        if(level.Type == "Resgate")
        {       
            BoardManager.Instance.SetBoardRescue(levelSelect, Heigth, Width, m_Icons);
        }
        if(level.Type == "Um_Dos_Doze_Trabalhos")
        {
            BoardManager.Instance.SetBoardWorks(levelSelect, Heigth, Width, m_Icons);
        }
        if (BoostManager.Instance.IsBoostOn())
        {
            BoostManager.Instance.ApplyEffects();
        }
        //m_CurrentState = GameState.STANDBY;
        StartCoroutine(DropCollumnAnimator(m_RefillDelay));
    }

    private IEnumerator BoardRunning(Vector2Int pOriginIndex, E_Direction pDirection)
    {
        m_CurrentState = GameState.RUNNING;

        int tOriginMatchAmount = 0;

        int tToMatchAmount = 0;

        Vector2Int pToIndex = pOriginIndex;

        SwitchDirection(pOriginIndex, pDirection, ref tOriginMatchAmount, ref pToIndex, ref tToMatchAmount);

        if(pToIndex != pOriginIndex)
            yield return SwapRoutine(pOriginIndex, pToIndex);

        yield return CheckChain(0);

        yield return RunSpecialActions();

        yield return RunIconsAnimations(0.3f);

        yield return GenerateSpecialIcon(pOriginIndex, tOriginMatchAmount,m_SpecialIconDelay);

        // second state resolve tags 
        // first action of second state destroy all icons (with tag MARK_TO_DESTROY)
        DestroyIcons();

        // the last state is refill board 
        DropCollumns();

        yield return DropCollumnAnimator(m_DropDelay);
       // add time to collumn drop
       //yield return new WaitForSeconds(m_RefillDelay);

        for (int i = 0; i < m_Icons.GetLength(0); i++)
        {
            if (m_Icons[i, 0] != null && m_Icons[i, 0].STag == "Down")
            {
                Destroy(m_Icons[i, 0].gameObject);
                m_Icons[i, 0] = null;
                ScoreManager.Instance.TargetLeft--;
            }
        }

        RefillBoard();

        yield return DropCollumnAnimator(m_RefillDelay);

        m_InitPosition = -Vector2.one;

        m_CurrentState = GameState.STANDBY;

    }

    public IEnumerator SwitchIcons()
    {
        m_CurrentState = GameState.RUNNING;
        Vector2 tPosition;
        Vector2Int firstIcon = Vector2Int.zero;
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() =>
        {
            if (OnMouseUp())
            {
                tPosition = MousePosition();
                firstIcon = FindIcon(tPosition);
                return IsInBoardRange(pX: firstIcon.x, pY: firstIcon.y) && m_Icons[firstIcon.x,firstIcon.y].StateIcon != BoardIcon.E_State.CANT_DESTROY && m_Icons[firstIcon.x, firstIcon.y].StateIcon != BoardIcon.E_State.OBJECTIVE;
            }
            return false;
        });
        Debug.Log("First");
        yield return new WaitForEndOfFrame();
        Vector2Int secondIcon = Vector2Int.zero;
        yield return new WaitUntil(() =>
        {
            if (OnMouseUp())
            {
                tPosition = MousePosition();
                secondIcon = FindIcon(tPosition);
                return IsInBoardRange(pX: secondIcon.x, pY: secondIcon.y) && m_Icons[secondIcon.x, secondIcon.y].StateIcon != BoardIcon.E_State.CANT_DESTROY && m_Icons[secondIcon.x, secondIcon.y].StateIcon != BoardIcon.E_State.OBJECTIVE;
            }
            return false;
        });
        Debug.Log("Segundo");
        // trocar as pecas
        SwapIcons(firstIcon, secondIcon);
        m_CurrentState = GameState.STANDBY;
    }
    public IEnumerator ZeusThunder()
    {
        m_CurrentState = GameState.RUNNING;
        Vector2 tPosition;
        Vector2Int firstIcon = Vector2Int.zero;
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() =>
        {
            if (OnMouseUp())
            {
                tPosition = MousePosition();
                firstIcon = FindIcon(tPosition);
                return IsInBoardRange(pX: firstIcon.x, pY: firstIcon.y) && m_Icons[firstIcon.x, firstIcon.y].StateIcon != BoardIcon.E_State.CANT_DESTROY && m_Icons[firstIcon.x, firstIcon.y].StateIcon != BoardIcon.E_State.OBJECTIVE;
            }
            return false;
        });
        Debug.Log("First");
        yield return new WaitForEndOfFrame();
        Vector2Int secondIcon = Vector2Int.zero;
        yield return new WaitUntil(() =>
        {
            if (OnMouseUp())
            {
                tPosition = MousePosition();
                secondIcon = FindIcon(tPosition);
                return IsInBoardRange(pX: secondIcon.x, pY: secondIcon.y) && m_Icons[secondIcon.x, secondIcon.y].StateIcon != BoardIcon.E_State.CANT_DESTROY && m_Icons[secondIcon.x, secondIcon.y].StateIcon != BoardIcon.E_State.OBJECTIVE;
            }
            return false;
        });
        Debug.Log("Segundo");
        yield return new WaitForEndOfFrame();
        Vector2Int thirdIcon = Vector2Int.zero;
        yield return new WaitUntil(() =>
        {
            if (OnMouseUp())
            {
                tPosition = MousePosition();
                thirdIcon = FindIcon(tPosition);
                return IsInBoardRange(pX: thirdIcon.x, pY: thirdIcon.y) && m_Icons[thirdIcon.x, thirdIcon.y].StateIcon != BoardIcon.E_State.CANT_DESTROY && m_Icons[thirdIcon.x, thirdIcon.y].StateIcon != BoardIcon.E_State.OBJECTIVE;
            }
            return false;
        });
        Debug.Log("Terceiro");

        m_Icons[firstIcon.x, firstIcon.y].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
        m_Icons[secondIcon.x, secondIcon.y].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
        m_Icons[thirdIcon.x, thirdIcon.y].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
        DestroyIcons();
        DropCollumns();
        yield return DropCollumnAnimator(m_DropDelay);

        // add time to collumn drop
        //yield return new WaitForSeconds(m_RefillDelay);

        for (int i = 0; i < m_Icons.GetLength(0); i++)
        {
            if (m_Icons[i, 0] != null && m_Icons[i, 0].STag == "Down")
            {
                Destroy(m_Icons[i, 0].gameObject);
                m_Icons[i, 0] = null;
                ScoreManager.Instance.TargetLeft--;
            }
        }

        RefillBoard();
        yield return DropCollumnAnimator(m_RefillDelay);
        m_CurrentState = GameState.STANDBY;
    }
    public IEnumerator PowerUp()
    {
        m_CurrentState = GameState.RUNNING;
        Vector2 tPosition;
        Vector2Int firstIcon = Vector2Int.zero;
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() =>
        {
            if (OnMouseUp())
            {
                tPosition = MousePosition();
                firstIcon = FindIcon(tPosition);
              
                if(m_Icons[firstIcon.x,firstIcon.y].Type == BoardIcon.E_Type.SPECIAL && m_Icons[firstIcon.x, firstIcon.y].StateIcon != BoardIcon.E_State.CANT_DESTROY)
                {
                    SpecialIcon tCombo = IconManager.Instance.GetSpecialIcon((m_Icons[firstIcon.x, firstIcon.y]));
                    if (tCombo != null)
                    {
                        List<SpecialIcon.Action> tBoardActions = tCombo.ActionsPowerUp;
                        if (tBoardActions != null)
                        {
                            for (int k = 0; k < tBoardActions.Count; k++)
                            {
                                int tIndexAction = k;
                                m_BoardModifies.Add(delegate
                                {
                                    BaseAction tBaseAction = BaseAction.GetActionObject(tBoardActions[tIndexAction].Type, tBoardActions[tIndexAction].ActionToRun);
                                    tBaseAction.Action(firstIcon.x, firstIcon.y, m_Icons);
                                });
                            }
                        }
                    }

                    Destroy(m_Icons[firstIcon.x, firstIcon.y].gameObject);
                    m_Icons[firstIcon.x, firstIcon.y] = null;
                }
                return true;
            }
            return false;
        });
        yield return CheckChain(0);

        yield return RunSpecialActions();

        yield return RunIconsAnimations(0.3f);

        // second state resolve tags 
        // first action of second state destroy all icons (with tag MARK_TO_DESTROY)
        DestroyIcons();

        // the last state is refill board 
        DropCollumns();

        // add time to collumn drop
        yield return DropCollumnAnimator(m_DropDelay);

        RefillBoard();
        yield return DropCollumnAnimator(m_RefillDelay);
        m_InitPosition = -Vector2.one;
        m_CurrentState = GameState.STANDBY;
    }
    #region Board Actions



    void SwitchDirection(Vector2Int pOriginIndex, E_Direction pDirection, ref int tOriginMatchAmount, ref Vector2Int pToIndex, ref int tToMatchAmount)
    {
        // first state mark all pieces with some tag
        switch (pDirection)
        {
            case E_Direction.STAY:
                // just click in icon
                BoardStay(pOriginIndex);

                tOriginMatchAmount = CountIconsInMatch(pOriginIndex);

                break;
            case E_Direction.LEFT:

                pToIndex = new Vector2Int(pOriginIndex.x - 1, pOriginIndex.y);

                tOriginMatchAmount = CountIconsInMatch(pOriginIndex);

                tToMatchAmount = CountIconsInMatch(pToIndex);

                break;
            case E_Direction.DOWN:

                pToIndex = new Vector2Int(pOriginIndex.x, pOriginIndex.y - 1);

                tOriginMatchAmount = CountIconsInMatch(pOriginIndex);

                tToMatchAmount = CountIconsInMatch(pToIndex);

                break;
            case E_Direction.UP:

                pToIndex = new Vector2Int(pOriginIndex.x, pOriginIndex.y + 1);

                tOriginMatchAmount = CountIconsInMatch(pOriginIndex);

                tToMatchAmount = CountIconsInMatch(pToIndex);

                break;
            case E_Direction.RIGHT:

                pToIndex = new Vector2Int(pOriginIndex.x + 1, pOriginIndex.y);

                tOriginMatchAmount = CountIconsInMatch(pOriginIndex);

                tToMatchAmount = CountIconsInMatch(pToIndex);

                break;
        }
    }

    IEnumerator RunSpecialActions()
    {
        for (int i = 0; i < m_BoardModifies.Count; i++)
        {
            m_BoardModifies[i].Invoke();
            yield return new WaitForEndOfFrame();
        }
        m_BoardModifies.Clear();
    }

    IEnumerator RunIconsAnimations(float pAnimationTime)
    {
        for (int i = 0; i < m_Icons.GetLength(0); i++)
        {
            for (int j = 0; j < m_Icons.GetLength(1); j++)
            {
                if(m_Icons[i,j]!=null)
                {
                    if (m_Icons[i, j].StateIcon == BoardIcon.E_State.MARK_TO_DESTROY && m_Icons[i, j] != null)
                    {
                        if(m_Icons[i,j].Durability==0)
                        {
                            VisualIcon tVisualIcon = m_Icons[i, j].GetComponent<VisualIcon>();
                            if (tVisualIcon != null)
                                tVisualIcon.ExplodeIcon();
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(pAnimationTime);
    }

    private IEnumerator GenerateSpecialIcon(Vector2Int pOriginIndex, int pOriginAmount,float timeDelay)
    {
        BoardIcon tIcon = m_Icons[pOriginIndex.x, pOriginIndex.y];

        Hashtable hash = new Hashtable();
        hash.Add("y", pOriginIndex.y + OffSet);
        hash.Add("x", pOriginIndex.x);
        hash.Add("easetype", "EaseOutBounce");
        hash.Add("time", timeDelay);

        if (pOriginAmount>=6)
        {
            for (int i = 0; i < m_Icons.GetLength(0); i++)
            {
                for (int j = 0; j < m_Icons.GetLength(1); j++)
                {
                    if (m_Icons[i, j] != null && m_Icons[i, j].STag == tIcon.STag && m_Icons[i, j].StateIcon == BoardIcon.E_State.MARK_TO_DESTROY)
                    {
                        //iTween.MoveTo(m_Icons[i, j].gameObject, tPosition, timeDelay);
                        iTween.MoveTo(m_Icons[i, j].gameObject, hash);
                    }
                }
            }
        }
        BoardIcon tGenerateBoardIcon = IconManager.Instance.GenerateSpecialIconByMatch(pOriginIndex.x, pOriginIndex.y + OffSet, pOriginAmount, tIcon.m_sTag);
        if (tGenerateBoardIcon != null)
        {     
            yield return new WaitForSeconds(timeDelay);
            Destroy(tIcon.gameObject);
            m_Icons[pOriginIndex.x, pOriginIndex.y] = tGenerateBoardIcon;
        }
        
    }

    void BoardStay(Vector2Int pIconIndex)
    {
        List<Vector2Int> tIcons = null;

        GetRegionIcons(pIconIndex.x, pIconIndex.y, ref tIcons);

        //Clicked Icon
        BoardIcon tIcon = m_Icons[pIconIndex.x, pIconIndex.y];

        //Debug.Log(tIcons.Count);
        if(!(tIcon.StateIcon == BoardIcon.E_State.CANT_DESTROY))
        {
            if (IsAMatch(tIcons) || IsSpecialIcon(tIcon))
            {
                MarkToDestroy(tIcons);
            }
            else
            {
                if(level.Type == "Um_Dos_Doze_Trabalhos")
                {
                    Shadowy(pIconIndex);
                }
                if(level.Type == "Sobre_O_Olhar_Da_Gorgona")
                {
                    Petrify(pIconIndex);
                }
            }
        }
        ReduceShadow();
        ReducePetrify();
    }
    void Petrify(Vector2Int pIconIndex)
    {
        if(IsInBoardRange(pIconIndex.x, pIconIndex.y) && m_Icons[pIconIndex.x, pIconIndex.y] != null && m_Icons[pIconIndex.x, pIconIndex.y].StateIcon != BoardIcon.E_State.CANT_DESTROY)
        {
            m_Icons[pIconIndex.x, pIconIndex.y].petrified = 3;
            m_Icons[pIconIndex.x, pIconIndex.y].StateIcon = BoardIcon.E_State.CANT_DESTROY;
            m_Icons[pIconIndex.x, pIconIndex.y].SetSprite(stone);
        }
    }
    void ReducePetrify()
    {
        for (int i = 0; i < m_Icons.GetLength(0); i++)
        {
            for (int j = 0; j < m_Icons.GetLength(1); j++)
            {
                if (m_Icons[i, j].SpriteRenderer.sprite == stone)
                {
                    m_Icons[i, j].petrified--;
                    if (m_Icons[i, j].petrified == 0)
                    {
                        m_Icons[i, j].StateIcon = BoardIcon.E_State.STAND_BY;
                        IconManager.Instance.ReturnOriginalSprite(m_Icons[i, j]);
                    }
                }
            }
        }
    }
    void ReduceShadow()
    {
        for (int i = 0; i < m_Icons.GetLength(0); i++)
        {
            for (int j = 0; j < m_Icons.GetLength(1); j++)
            {
                if(m_Icons[i,j].StateIcon == BoardIcon.E_State.SHADOW)
                {
                    m_Icons[i, j].shadowy--;
                    if(m_Icons[i,j].shadowy == 0)
                    {
                        m_Icons[i, j].StateIcon = BoardIcon.E_State.STAND_BY;
                        IconManager.Instance.ReturnOriginalSprite(m_Icons[i, j]);
                    }
                }
            }
        }
    }
    void Shadowy(Vector2Int pIconIndex)
    {
        for (int i = pIconIndex.x-1; i <pIconIndex.x+2 ; i++)
        {
            if(IsInBoardRange(i,pIconIndex.y) && m_Icons[i,pIconIndex.y]!=null && m_Icons[i,pIconIndex.y].StateIcon != BoardIcon.E_State.CANT_DESTROY)
            {
                m_Icons[i,pIconIndex.y].shadowy = 4;
                m_Icons[i, pIconIndex.y].StateIcon = BoardIcon.E_State.SHADOW;
                m_Icons[i, pIconIndex.y].SetSprite(shadow);
            }
        }
        for (int i = pIconIndex.y - 1; i < pIconIndex.y + 2; i++)
        {
            if (IsInBoardRange(pIconIndex.x,i) && m_Icons[pIconIndex.x, i] != null && m_Icons[pIconIndex.x,i].StateIcon != BoardIcon.E_State.CANT_DESTROY)
            {
                m_Icons[pIconIndex.x, i].shadowy = 4;
                m_Icons[pIconIndex.x, i].StateIcon = BoardIcon.E_State.SHADOW;
                m_Icons[pIconIndex.x, i].SetSprite(shadow);
            }
        }
    }
    void DropCollumns()
    {
        for (int i = 0; i < m_Icons.GetLength(0); i++)
        {
            for (int j = 0; j < m_Icons.GetLength(1); j++)
            {
                if (m_Icons[i, j] == null)
                    DropCollumn(i, j);
            }
        }

    }

    void DropCollumn(int pX, int pY)
    {

        int tDropCount = 0;

        for (int i = pY; i < Heigth - 1; i++)
        {
            if (m_Icons[pX, i] == null)
            {
                tDropCount++;
            }
            else
                break;
        }

        for (int i = pY; i < Heigth - tDropCount; i++)
        {
          

            int tAboveIndex = - 1;

            int indestructable = 0;

            for (int j = i; j < Heigth - tDropCount; j++)
            {
                tAboveIndex = j + tDropCount;
                BoardIcon tIconAbove = m_Icons[pX, tAboveIndex];
                if(tIconAbove != null && tIconAbove.StateIcon == BoardIcon.E_State.CANT_DESTROY)
                {
                    indestructable++;
                    continue;
                }
                else
                {
                    break;
                }
            }
            //if (tAboveIndex >= Heigth - tDropCount + 1)
               // break;
            if(m_Icons[pX,tAboveIndex] !=null && m_Icons[pX, tAboveIndex].StateIcon != BoardIcon.E_State.CANT_DESTROY)
            {
                BoardIcon tIcon = m_Icons[pX, i];

                if (tIcon != null && tIcon.StateIcon == BoardIcon.E_State.CANT_DESTROY)
                    continue;

                m_Icons[pX, i] = m_Icons[pX, tAboveIndex];

                m_Icons[pX, tAboveIndex] = tIcon;

                if (m_Icons[pX, i] != null)
                {
                    m_Icons[pX, i].row = pX;
                    m_Icons[pX, i].colunm = tAboveIndex - tDropCount - indestructable + OffSet;
                }
            }
            
        }

        
    }
    private IEnumerator DropCollumnAnimator(float timeDelay)
    {
        for (int i = 0; i < m_Icons.GetLength(0); i++)
        {
            for (int j = 0; j < m_Icons.GetLength(1); j++)
            {
                if (m_Icons[i, j] != null)
                {
                    Vector2 tPosition = new Vector2(m_Icons[i, j].row, m_Icons[i, j].colunm);
                    if (Vector2.Distance(m_Icons[i, j].transform.position, tPosition) > 0.5f)
                    {
                        Hashtable hash = new Hashtable();
                        hash.Add("y", tPosition.y);
                        hash.Add("x", tPosition.x);
                        hash.Add("easetype", "EaseOutBounce");
                        hash.Add("time", timeDelay);
                        //iTween.MoveTo(m_Icons[i, j].gameObject, tPosition, timeDelay);
                        iTween.MoveTo(m_Icons[i, j].gameObject,hash);
                    }
                }
            }
        }
        yield return new WaitForSeconds(timeDelay);
    }
    void Bouncing()
    {

    }
    void DestroyIcons()
    {
        bool validMove = false;
        for (int i = 0; i < m_Icons.GetLength(0); i++)
        {
            for (int j = 0; j < m_Icons.GetLength(1); j++)
            {
                if(m_Icons[i, j] != null)
                {
                    if (m_Icons[i, j].StateIcon == BoardIcon.E_State.MARK_TO_DESTROY)
                    {
                        m_Icons[i, j].Durability--;
                        m_Icons[i, j].StateIcon = BoardIcon.E_State.STAND_BY;
                        if (m_Icons[i,j].Durability==0)
                        {
                            ScoreManager.Instance.AddPoint(50);
                            for (int z = 0; z < GameManager.Instance.m_IconToDestroy.Count; z++)
                            {
                                if (m_Icons[i,j].STag == GameManager.Instance.m_IconToDestroy[z].Tag)
                                {
                                    ScoreManager.Instance.ReduceNumberTarget(1,z);
                                }
                            }
                            Destroy(m_Icons[i, j].gameObject);
                            m_Icons[i, j] = null;
                        }
                        validMove = true;
                        
                    }
                }
            }
        }
        if (validMove)
            ScoreManager.Instance.MovesLeft -= 1;

    }

    private void RefillBoard()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Heigth; j++)
            {
                if (m_Icons[i, j] == null)
                {
                    BoardIcon tIcon = IconManager.Instance.GenerateRandomIcon(i, j,level.Scenario);
                    tIcon.colunm = j + OffSet;
                    tIcon.row = i;
                    tIcon.transform.parent = transform;
                    m_Icons[i, j] = tIcon;
                }
            }
        }
    }

    private IEnumerator SwapRoutine(Vector2Int pFrom, Vector2Int pTo)
    {
        if (IsInBoardRange(pTo.x, pTo.y))
        {
            SwapIcons(pFrom, pTo);

            yield return new WaitForSeconds(m_SwapDelay);


            //Clicked Icon
            BoardIcon tIconFrom = m_Icons[pFrom.x, pFrom.y];
            BoardIcon tIconTo = m_Icons[pTo.x, pTo.y];

            //Debug.Log(tIcons.Count);

            bool tReSwap = true;

            if (IsSpecialIcon(tIconFrom) && IsSpecialIcon(tIconTo))
            {
                //tIconFrom.StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
                //tIconTo.StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;

                Combo tCombo = IconManager.Instance.GetCombo(tIconFrom, tIconTo);
                if (tCombo != null)
                {
                    List<Icon.Action> tBoardActions = tCombo.Actions;
                    if (tBoardActions != null)
                    {
                        int tX = pTo.x;
                        int tY = pTo.y;
                        

                        for (int k = 0; k < tBoardActions.Count; k++)
                        {
                            int tIndexAction = k;
                            m_BoardModifies.Add(delegate
                            {
                                BaseAction tBaseAction = BaseAction.GetActionObject(tBoardActions[tIndexAction].Type, tBoardActions[tIndexAction].ActionToRun);
                                tBaseAction.Action(tX, tY, m_Icons);
                            });
                        }
                    }
                }
                
                Destroy(tIconFrom.gameObject);
                tIconFrom = null;
                Destroy(tIconTo.gameObject);
                tIconTo = null;
                tReSwap = false;
            }


            if (tReSwap)
            {
                SwapIcons(pTo, pFrom);
            }
            ReduceShadow();
            ReducePetrify();
        }
    }

    private IEnumerator CheckChain(int a)
    {
        yield return new WaitForSeconds(0.2f);
        yield return RunSpecialActions();
        if (a!=1)
        {
            a = 1;
            for (int i = 0; i < m_Icons.GetLength(0); i++)
            {
                for (int j = 0; j < m_Icons.GetLength(1); j++)
                {
                    if (m_Icons[i, j] != null)
                    {
                        if (m_Icons[i, j].StateIcon == BoardIcon.E_State.MARK_TO_DESTROY && m_Icons[i, j].Type == BoardIcon.E_Type.SPECIAL)
                        {
                            List<Icon.Action> tBoardActions = m_Icons[i, j].Actions;
                            if (tBoardActions != null)
                            {
                                for (int k = 0; k < tBoardActions.Count; k++)
                                {
                                    int tIndexAction = k;
                                    m_BoardModifies.Add(delegate
                                    {
                                        BaseAction tBaseAction = BaseAction.GetActionObject(tBoardActions[tIndexAction].Type, tBoardActions[tIndexAction].ActionToRun);
                                        tBaseAction.Action(i, j, m_Icons);
                                    });
                                    yield return RunSpecialActions();
                                    a = 0;
                                }
                            }
                        }
                    }
                }
            }
            CheckChain(a);
        }
    }

    private void SwapIcons(Vector2Int pFrom, Vector2Int pTo)
    {
        m_Icons[pFrom.x, pFrom.y].row = pTo.x;
        m_Icons[pFrom.x, pFrom.y].colunm = pTo.y;

        m_Icons[pTo.x, pTo.y].row = pFrom.x;
        m_Icons[pTo.x, pTo.y].colunm = pFrom.y;

        BoardIcon tIcon = m_Icons[pFrom.x, pFrom.y];

        m_Icons[pFrom.x, pFrom.y] = m_Icons[pTo.x, pTo.y];

        m_Icons[pTo.x, pTo.y] = tIcon;
    }

    /*public void GetBoardScenario(int level)
    {
        string scenario = BoardManager.Instance.Nivel[level];
        string type = BoardManager.Instance.Type[level];
        if(scenario=="APOCALIPTICO")
        {
            m_CurrentScenario = BoardScenario.APOCALIPTICO;
            if(type == "Resgate")
            {
                m_CurrentType = BoardType.RESGATE;
            }
            else
            {
                m_CurrentType = BoardType.DESATIVAR_BOMBA;
            }
        }
        else
        {
            m_CurrentScenario = BoardScenario.GREGO;
            if(type == "Um_Dos_Doze_Trabalhos")
            {
                m_CurrentType = BoardType.UM_DOS_DOZE_TRABALHOS;
            }
            else
            {
                m_CurrentType = BoardType.SOB_O_OLHAR_DA_GORGONA;
            }
        }
    }*/
    #endregion

    #region Board Methods 

    int CountIconsInMatch(Vector2Int pIconIndex)
    {
        List<Vector2Int> tIcons = null;

        GetRegionIcons(pIconIndex.x, pIconIndex.y, ref tIcons);

        return tIcons.Count;
    }

    void MarkToDestroy(List<Vector2Int> pIcons)
    {
        for (int i = 0; i < pIcons.Count; i++)
        {
            Vector2Int tIndex = pIcons[i];
            BoardIcon tIcon = m_Icons[tIndex.x, tIndex.y];
            if(tIcon !=null)
            {
                if (tIcon.StateIcon != BoardIcon.E_State.CANT_DESTROY)
                {
                    List<Icon.Action> tBoardActions = tIcon.Actions;
                    if (tBoardActions != null)
                    {
                        int tX = tIndex.x;
                        int tY = tIndex.y;

                        for (int k = 0; k < tBoardActions.Count; k++)
                        {
                            int tIndexAction = k;
                            m_BoardModifies.Add(delegate
                            {
                                BaseAction tBaseAction = BaseAction.GetActionObject(tBoardActions[tIndexAction].Type, tBoardActions[tIndexAction].ActionToRun);
                                tBaseAction.Action(tX, tY, m_Icons);
                            });
                        }
                    } 
                    if(tIcon.Type == BoardIcon.E_Type.SPECIAL)
                    {
                        Destroy(tIcon.gameObject);
                        tIcon = null;
                    }
                    else
                    {
                        tIcon.StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
                    }
                }
            }
        }
    }

   

    List<Vector2Int> GetRegionIcons(int pX, int pY, ref List<Vector2Int> pIcons)
    {
        if (pIcons == null)
            pIcons = new List<Vector2Int>();

        if (!IsInBoardRange(pX, pY))
            return new List<Vector2Int>();

        if (!pIcons.Contains(new Vector2Int(pX, pY)))
            pIcons.Add(new Vector2Int(pX, pY));
        else
            return new List<Vector2Int>();

        BoardIcon tIcon = m_Icons[pX, pY];

        if (tIcon != null)
        {
            for (int i = pX - 1; i <= pX + 1; i++)
            {
                if (IsInBoardRange(i, pY) && i != pX && m_Icons[i, pY].STag == tIcon.STag && !pIcons.Contains(new Vector2Int(i, pY)))
                {
                    GetRegionIcons(i, pY, ref pIcons);

                }
                if (IsInBoardRange(i, pY) && i != pX && m_Icons[i, pY].STag == "Rescue" && !pIcons.Contains(new Vector2Int(i, pY)))
                {
                    GetRegionIcons(i, pY, ref pIcons);
                }
            }

            for (int j = pY - 1; j <= pY + 1; j++)
            {
                if (IsInBoardRange(pX, j) && j != pY && m_Icons[pX, j].STag == tIcon.STag && !pIcons.Contains(new Vector2Int(pX, j)))
                {
                    GetRegionIcons(pX, j, ref pIcons);
                }
                if (IsInBoardRange(pX, j) && j != pY && m_Icons[pX, j].STag == "Rescue" && !pIcons.Contains(new Vector2Int(pX, j)))
                {
                    GetRegionIcons(pX, j, ref pIcons);
                }
            }
        }

        return pIcons;
    }

    bool IsOverUI()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0);
    }

    bool OnMouseDown()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    bool OnMouseUp()
    {
        return Input.GetKeyUp(KeyCode.Mouse0);
    }

    Vector2 MousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    Vector2Int FindIcon(Vector2 pPosi)
    {
        for (int i = 0; i < m_Icons.GetLength(0); i++)
        {
            for (int j = 0; j < m_Icons.GetLength(1); j++)
            {
                Vector2 tCurrentPosition = Vector2.one;
                tCurrentPosition.x *= i;
                tCurrentPosition.y *= (j + OffSet);
                float tDistance = Vector2.Distance(tCurrentPosition, pPosi);
                if (tDistance < 0.5)
                    return new Vector2Int(i, j);
            }
        }
        return new Vector2Int(-1, -1);
    }

    private E_Direction GetDirection(Vector2 pInitPosi, Vector2 pEndPosi)
    {
        if (Vector2.Distance(pInitPosi, pEndPosi) < 0.5)
            return E_Direction.STAY;

        float tAngle = Mathf.Atan2(pEndPosi.y - pInitPosi.y, pEndPosi.x - pInitPosi.x) * 180 / Mathf.PI;

        if (tAngle > -45 && tAngle <= 45)//right swap
        {
            return E_Direction.RIGHT;
        }
        else if (tAngle > 45 && tAngle <= 135)//up swap
        {
            return E_Direction.UP;
        }
        else if ((tAngle > 135 || tAngle <= -135))//left swap
        {
            return E_Direction.LEFT;
        }
        else //if (swipeAngle < -45 && swipeAngle >= -135 && colunm > 0)//down swap
        {
            return E_Direction.DOWN;
        }

    }

    bool IsInBoardRange(int pX, int pY)
    {
        return pX >= 0 && pX < Width && pY >= 0 && pY < Heigth;
    }

    bool IsAMatch(List<Vector2Int> pIcons)
    {
        return pIcons.Count >= 3;
    }

    bool IsSpecialIcon(BoardIcon pIcon)
    {
        
        return pIcon!=null && pIcon.Type == BoardIcon.E_Type.SPECIAL;
    }


    #endregion

    #region Member Class Methods

    public List<Action> Actions
    {
        get
        {
            return m_Actions;
        }

        set
        {
            m_Actions = value;
        }
    }

    public Action TriggerMatch
    {
        get
        {
            return m_TriggerMatch;
        }

        set
        {
            m_TriggerMatch = value;
        }
    }

    public int OffSet
    {
        get
        {
            return m_OffSet;
        }

        set
        {
            m_OffSet = value;
        }
    }

    public int Heigth
    {
        get
        {
            return m_Heigth;
        }

        set
        {
            m_Heigth = value;
        }
    }

    public int Width
    {
        get
        {
            return m_Width;
        }

        set
        {
            m_Width = value;
        }
    }

    #endregion
}


// old version
// private float moves;
//private float score = 0;

//
//public GameObject tilePrefab;
//private BackgroundTile[,] allTiles;
//public GameObject blockIcon;
//public GameObject[,] allicons;

//public int currentX;
//public int currentY;
//public int currentDestroyed;
//public float swipeAngle = 0;
//private GameObject swapingIcon;

//public bool DestroyMatch()
//{
//    if (CheckIfCanDestroy())
//    {
//        int tempIconToUse = 6;
//        if (allicons[currentX, currentY].GetComponent<Icon>().m_sTag == "BluePotion")
//        {
//            tempIconToUse = 6;
//        }
//        if (allicons[currentX, currentY].GetComponent<Icon>().m_sTag == "GreenPotion")
//        {
//            tempIconToUse = 7;
//        }
//        if (allicons[currentX, currentY].GetComponent<Icon>().m_sTag == "RedPotion")
//        {
//            tempIconToUse = 8;
//        }
//        if (allicons[currentX, currentY].GetComponent<Icon>().m_sTag == "YellowPotion")
//        {
//            tempIconToUse = 9;
//        }
//        TriggerMatch.Invoke();
//        for (int i = 0; i < width; i++)
//        {
//            for (int j = 0; j < height; j++)
//            {
//                if (allicons[i, j] != null)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().isMatch && !(allicons[i, j].GetComponent<Icon>().isIndestructable))
//                    {
//                        Destroy(allicons[i, j]);
//                        allicons[i, j] = null;
//                    }
//                }
//            }
//        }
//        if (currentDestroyed == 6)
//        {
//            Vector2 tempPosition = new Vector2(currentX, currentY);
//            GameObject newIcon = Instantiate(icons[4], tempPosition, Quaternion.identity);
//            allicons[currentX, currentY] = newIcon;
//            newIcon.GetComponent<Icon>().colunm = currentY;
//            newIcon.GetComponent<Icon>().row = currentX;
//            newIcon.transform.parent = transform;
//        }
//        if (currentDestroyed == 7)
//        {
//            Vector2 tempPosition = new Vector2(currentX, currentY);
//            GameObject newIcon = Instantiate(icons[5], tempPosition, Quaternion.identity);
//            allicons[currentX, currentY] = newIcon;
//            newIcon.GetComponent<Icon>().colunm = currentY;
//            newIcon.GetComponent<Icon>().row = currentX;
//            newIcon.transform.parent = transform;
//        }
//        if (currentDestroyed >= 8)
//        {
//            Vector2 tempPosition = new Vector2(currentX, currentY);
//            GameObject newIcon = Instantiate(icons[tempIconToUse], tempPosition, Quaternion.identity);
//            allicons[currentX, currentY] = newIcon;
//            newIcon.GetComponent<Icon>().colunm = currentY;
//            newIcon.GetComponent<Icon>().row = currentX;
//            newIcon.transform.parent = transform;
//        }

//        StartCoroutine(FinishMatch());
//        return true;
//    }
//    StartCoroutine(FinishMatch());
//    return false;
//}

//public void SetIsMatchFalse(int targetX, int targetY)
//{
//    if (allicons[targetX, targetY] != null)
//        allicons[targetX, targetY].GetComponent<Icon>().isMatch = false;

//    if (targetX > 0)
//    {
//        if (allicons[targetX - 1, targetY] != null)
//        {
//            allicons[targetX - 1, targetY].GetComponent<Icon>().isMatch = false;
//        }
//    }
//    if (targetX < width - 1)
//    {
//        if (allicons[targetX + 1, targetY] != null)
//        {
//            allicons[targetX + 1, targetY].GetComponent<Icon>().isMatch = false;
//        }
//    }
//    if (targetY < height - 1)
//    {
//        if (allicons[targetX, targetY + 1] != null)
//        {
//            allicons[targetX, targetY + 1].GetComponent<Icon>().isMatch = false;
//        }
//    }
//    if (targetY > 0)
//    {
//        if (allicons[targetX, targetY - 1] != null)
//        {
//            allicons[targetX, targetY - 1].GetComponent<Icon>().isMatch = false;
//        }
//    }
//}

//public bool CheckIfCanDestroy()
//{
//    int nrmTitle = 0;
//    for (int i = 0; i < width; i++)
//    {
//        for (int j = 0; j < height; j++)
//        {
//            if (allicons[i, j] != null)
//            {
//                if (allicons[i, j].GetComponent<Icon>().isMatch)
//                {
//                    nrmTitle++;
//                }
//            }
//        }
//    }
//    currentDestroyed = nrmTitle;
//    if (nrmTitle >= 3)
//    {
//        Scoring(nrmTitle);
//        return true;
//    }
//    return false;
//}

//public IEnumerator FinishMatch()
//{
//    yield return new WaitForEndOfFrame();
//    yield return RunActions();

//    for (int i = 0; i < width; i++)
//    {
//        for (int j = 0; j < height; j++)
//        {
//            SetIsMatchFalse(i, j);
//        }
//    }
//    yield return DropColumns();


//}

//private IEnumerator RunActions()
//{
//    if (m_pActions == null)
//        yield break;

//    foreach (System.Action tAction in m_pActions)
//    {
//        tAction.Invoke();
//    }
//    m_pActions = new List<System.Action>();
//}

//private IEnumerator DropColumns()
//{
//    int nullCount = 0;
//    int indestructable = 0;
//    for (int i = 0; i < width; i++)
//    {
//        for (int j = 0; j < height; j++)
//        {
//            if (allicons[i, j] == null)
//            {
//                nullCount++;
//            }
//            else if (nullCount > 0)
//            {
//                if (allicons[i, j].GetComponent<Icon>().isIndestructable)
//                {
//                    indestructable++;
//                }
//                else {
//                    // GAMBIARRA DO KRL
//                    if ((j - (nullCount + indestructable)) >= 0 && (j - (nullCount + indestructable)) < height && allicons[i, j - (nullCount + indestructable)] != null && allicons[i, j - (nullCount + indestructable)].GetComponent<Icon>().isIndestructable)
//                    {

//                        indestructable = 0;
//                    }
//                    allicons[i, j].GetComponent<Icon>().colunm -= nullCount + indestructable;
//                    allicons[i, j] = null;
//                }
//            }
//        }

//        indestructable = 0;
//        nullCount = 0;
//    }
//    yield return new WaitForSeconds(.4f);
//    StartCoroutine(FillBoard());
//}



//private IEnumerator FillBoard()
//{
//    RefillBoard();
//    yield return new WaitForSeconds(.5f);

//}

//public void UpdateMoveScore()
//{
//    m_pGamePlayUI.TScore.text = " " + score;
//    moves--;

//    if (moves >= 0) {
//        m_pGamePlayUI.TMoves.text = " " + moves;
//    }
//}

//public void FindMatch(int targetX, int targetY)
//{
//    GameObject tCurrentIcon = allicons[targetX, targetY];

//    // check if  has a match in left of current icon
//    if (!allicons[targetX, targetY].GetComponent<Icon>().isSpecial)
//    {
//        if (targetX > 0)
//        {
//            if (allicons[targetX - 1, targetY] != null)
//            {
//                GameObject leftIcon1 = allicons[targetX - 1, targetY];

//                if (leftIcon1.GetComponent<Icon>().STag == tCurrentIcon.GetComponent<Icon>().STag)
//                {
//                    leftIcon1.GetComponent<Icon>().isMatch = true;
//                    //FindMatch(leftIcon1);

//                }
//            }
//        }
//        // check if  has a match in right of current icon
//        if (targetX < width - 1)
//        {
//            if (allicons[targetX + 1, targetY] != null)
//            {
//                GameObject rightIcon1 = allicons[targetX + 1, targetY];
//                if (rightIcon1.GetComponent<Icon>().STag == tCurrentIcon.GetComponent<Icon>().STag)
//                {
//                    rightIcon1.GetComponent<Icon>().isMatch = true;
//                    //FindMatch(rightIcon1);
//                }
//            }
//        }
//        // check if  has a match in upward of current icon
//        if (targetY < height - 1)
//        {
//            if (allicons[targetX, targetY + 1] != null)
//            {
//                GameObject upIcon1 = allicons[targetX, targetY + 1];

//                if (upIcon1.GetComponent<Icon>().STag == tCurrentIcon.GetComponent<Icon>().STag)
//                {
//                    upIcon1.GetComponent<Icon>().isMatch = true;
//                    //FindMatch(upIcon1);

//                }

//            }
//        }
//        // check if  has a match in downward of current icon
//        if (targetY > 0)
//        {
//            if (allicons[targetX, targetY - 1] != null)
//            {
//                GameObject downIcon1 = allicons[targetX, targetY - 1];
//                if (downIcon1.GetComponent<Icon>().STag == tCurrentIcon.GetComponent<Icon>().STag)
//                {
//                    downIcon1.GetComponent<Icon>().isMatch = true;
//                    //FindMatch(downIcon1);

//                }
//            }
//        }
//    }
//    if (allicons[targetX, targetY].GetComponent<Icon>().isSpecial)
//    {
//        SpecialEffect(targetX, targetY);
//        StartCoroutine(FinishMatch());
//    }

//}

//public void Scoring(int pTitles)
//{
//    if (pTitles == 3)
//    {
//        scoreInst = 100;
//    }
//    else
//    {
//        scoreInst = 100 * (pTitles - 2);
//    }

//    score += scoreInst;
//    UpdateMoveScore();
//}

//public void DestroyRow(int pRow, int targetY)
//{
//    allicons[pRow, targetY].GetComponent<Icon>().isSpecial = false;
//    for (int j = 0; j < height; j++)
//    {
//        if (allicons[pRow, j] != null)
//        {
//            if (allicons[pRow, j] == allicons[pRow, targetY])
//            {
//                Destroy(allicons[pRow, j]);
//            }
//            else if (allicons[pRow, j].GetComponent<Icon>().isSpecial)
//            {
//                SpecialEffect(pRow, j);
//            }
//            else
//            {
//                Destroy(allicons[pRow, j]);
//            }
//        }
//    }
//}

//public void DestroyCollum(int targetX, int pCollum)
//{
//    allicons[targetX, pCollum].GetComponent<Icon>().isSpecial = false;
//    for (int i = 0; i < width; i++)
//    {
//        if (allicons[i, pCollum] != null)
//        {
//            if (allicons[i, pCollum] == allicons[targetX, pCollum])
//            {
//                Destroy(allicons[i, pCollum]);
//            }
//            else if (allicons[i, pCollum].GetComponent<Icon>().isSpecial)
//            {
//                SpecialEffect(i, pCollum);
//            }
//            else
//            {
//                Destroy(allicons[i, pCollum]);
//            }
//        }
//    }
//}

//// destroy a area by a position and um integer pArea ex: pArea = 3 then 3x3 in originx, originy was been destroyed 
//public void DestroyArea(int pOriginX, int pOriginY, int pArea)
//{
//    allicons[pOriginX, pOriginY].GetComponent<Icon>().isSpecial = false;
//    for (int i = pOriginX - pArea; i <= pOriginX + pArea; i++)
//    {
//        for (int j = pOriginY - pArea; j <= pOriginY + pArea; j++)
//        {
//            if (IsOutOfBoardRange(i, j))
//            {
//                continue; //go to next interation 
//            }

//            if (allicons[i, j] != null)
//            {
//                if (allicons[i, j] == allicons[pOriginX, pOriginY])
//                {
//                    Destroy(allicons[i, j]);
//                }
//                else if (allicons[i, j].GetComponent<Icon>().isSpecial)
//                {
//                    SpecialEffect(i, j);
//                }
//                else
//                {
//                    Destroy(allicons[i, j]);
//                }
//            }
//        }
//    }
//}

//public bool IsOutOfBoardRange(int pPositionX, int pPositionY)
//{
//    return !(pPositionX >= 0 && pPositionX < width && pPositionY >= 0 && pPositionY < height);
//}


//public void DoCombo(int targetX, int targetY, int originX, int originY)
//{
//    GameObject origin = allicons[originX, originY];
//    GameObject second = allicons[targetX, targetY];
//    origin.GetComponent<Icon>().isSpecial = false;
//    second.GetComponent<Icon>().isSpecial = false;
//    if (origin.GetComponent<Icon>().STag == "6Bomb")
//    {
//        if (second.GetComponent<Icon>().STag == "6Bomb")
//        {
//            DestroyRow(originX, originY);
//            DestroyCollum(originX, originY);
//        }
//        else if (second.GetComponent<Icon>().STag == "7Bomb")
//        {
//            DestroyRow(originX + 1, originY);
//            DestroyRow(originX, originY);
//            DestroyRow(originX - 1, originY);
//            DestroyCollum(originX, originY + 1);
//            DestroyCollum(originX, originY);
//            DestroyCollum(originX, originY - 1);
//        }
//        else if (second.GetComponent<Icon>().STag == "8BombBlue")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "BluePotion")
//                    {
//                        int direction = Random.Range(0, 2);
//                        if (direction == 1)
//                        {
//                            DestroyRow(i, j);
//                        }
//                        else if (direction == 0)
//                        {
//                            DestroyCollum(i, j);
//                        }
//                    }
//                }
//            }
//        }
//        else if (second.GetComponent<Icon>().STag == "8BombGreen")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "GreenPotion")
//                    {
//                        int direction = Random.Range(0, 2);
//                        if (direction == 1)
//                        {
//                            DestroyRow(i, j);
//                        }
//                        else if (direction == 0)
//                        {
//                            DestroyCollum(i, j);
//                        }
//                    }
//                }
//            }
//        }
//        else if (second.GetComponent<Icon>().STag == "8BombRed")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "RedPotion")
//                    {
//                        int direction = Random.Range(0, 2);
//                        if (direction == 1)
//                        {
//                            DestroyRow(i, j);
//                        }
//                        else if (direction == 0)
//                        {
//                            DestroyCollum(i, j);
//                        }
//                    }
//                }
//            }
//        }
//        else if (second.GetComponent<Icon>().STag == "8BombYellow")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "YellowPotion")
//                    {
//                        int direction = Random.Range(0, 2);
//                        if (direction == 1)
//                        {
//                            DestroyRow(i, j);
//                        }
//                        else if (direction == 0)
//                        {
//                            DestroyCollum(i, j);
//                        }
//                    }
//                }
//            }
//        }
//    }
//    if (origin.GetComponent<Icon>().STag == "7Bomb")
//    {
//        if (second.GetComponent<Icon>().STag == "6Bomb")
//        {
//            DestroyRow(originX + 1, originY);
//            DestroyRow(originX, originY);
//            DestroyRow(originX - 1, originY);
//            DestroyCollum(originX, originY + 1);
//            DestroyCollum(originX, originY);
//            DestroyCollum(originX, originY - 1);
//        }
//        else if (second.GetComponent<Icon>().STag == "7Bomb")
//        {
//            DestroyArea(originX, originY, 3);
//        }
//        else if (second.GetComponent<Icon>().STag == "8BombBlue")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "BluePotion")
//                    {
//                        DestroyArea(i, j, 1);
//                    }
//                }
//            }
//        }
//        else if (second.GetComponent<Icon>().STag == "8BombGreen")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "GreenPotion")
//                    {
//                        DestroyArea(i, j, 1);
//                    }
//                }
//            }
//        }
//        else if (second.GetComponent<Icon>().STag == "8BombRed")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "RedPotion")
//                    {
//                        DestroyArea(i, j, 1);
//                    }
//                }
//            }
//        }
//        else if (second.GetComponent<Icon>().STag == "8BombYellow")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "YellowPotion")
//                    {
//                        DestroyArea(i, j, 1);
//                    }
//                }
//            }
//        }
//    }
//    if (origin.GetComponent<Icon>().STag == "8BombBlue")
//    {
//        if (second.GetComponent<Icon>().STag == "6Bomb")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "BluePotion")
//                    {
//                        int direction = Random.Range(0, 2);
//                        if (direction == 1)
//                        {
//                            DestroyRow(i, j);
//                        }
//                        else if (direction == 0)
//                        {
//                            DestroyCollum(i, j);
//                        }
//                    }
//                }
//            }
//        }
//        else if (second.GetComponent<Icon>().STag == "7Bomb")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "BluePotion")
//                    {
//                        DestroyArea(i, j, 1);
//                    }
//                }
//            }
//        }
//        else if (second.GetComponent<Icon>().STag == "8BombBlue" || second.GetComponent<Icon>().STag == "8BombGreen" || second.GetComponent<Icon>().STag == "8BombRed" || second.GetComponent<Icon>().STag == "8BombYellow")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    Destroy(allicons[i, j]);
//                }
//            }
//        }
//    }
//    if (origin.GetComponent<Icon>().STag == "8BombGreen")
//    {
//        if (second.GetComponent<Icon>().STag == "6Bomb")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "GreenPotion")
//                    {
//                        int direction = Random.Range(0, 2);
//                        if (direction == 1)
//                        {
//                            DestroyRow(i, j);
//                        }
//                        else if (direction == 0)
//                        {
//                            DestroyCollum(i, j);
//                        }
//                    }
//                }
//            }
//        }
//        else if (second.GetComponent<Icon>().STag == "7Bomb")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "GreenPotion")
//                    {
//                        DestroyArea(i, j, 1);
//                    }
//                }
//            }
//        }
//        else if (second.GetComponent<Icon>().STag == "8BombBlue" || second.GetComponent<Icon>().STag == "8BombGreen" || second.GetComponent<Icon>().STag == "8BombRed" || second.GetComponent<Icon>().STag == "8BombYellow")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    Destroy(allicons[i, j]);
//                }
//            }
//        }
//    }
//    if (origin.GetComponent<Icon>().STag == "8BombRed")
//    {
//        if (second.GetComponent<Icon>().STag == "6Bomb")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "RedPotion")
//                    {
//                        int direction = Random.Range(0, 2);
//                        if (direction == 1)
//                        {
//                            DestroyRow(i, j);
//                        }
//                        else if (direction == 0)
//                        {
//                            DestroyCollum(i, j);
//                        }
//                    }
//                }
//            }
//        }
//        else if (second.GetComponent<Icon>().STag == "7Bomb")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "RedPotion")
//                    {
//                        DestroyArea(i, j, 1);
//                    }
//                }
//            }
//        }
//        else if (second.GetComponent<Icon>().STag == "8BombBlue" || second.GetComponent<Icon>().STag == "8BombGreen" || second.GetComponent<Icon>().STag == "8BombRed" || second.GetComponent<Icon>().STag == "8BombYellow")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    Destroy(allicons[i, j]);
//                }
//            }
//        }
//    }
//    if (origin.GetComponent<Icon>().STag == "8BombYellow")
//    {
//        if (second.GetComponent<Icon>().STag == "6Bomb")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "YellowPotion")
//                    {
//                        int direction = Random.Range(0, 2);
//                        if (direction == 1)
//                        {
//                            DestroyRow(i, j);
//                        }
//                        else if (direction == 0)
//                        {
//                            DestroyCollum(i, j);
//                        }
//                    }
//                }
//            }
//        }
//        else if (second.GetComponent<Icon>().STag == "7Bomb")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "YellowPotion")
//                    {
//                        DestroyArea(i, j, 1);
//                    }
//                }
//            }
//        }
//        else if (second.GetComponent<Icon>().STag == "8BombBlue" || second.GetComponent<Icon>().STag == "8BombGreen" || second.GetComponent<Icon>().STag == "8BombRed" || second.GetComponent<Icon>().STag == "8BombYellow")
//        {
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    Destroy(allicons[i, j]);
//                }
//            }
//        }
//    }
//    if (origin != null)
//    {
//        Destroy(allicons[originX, originY]);
//    }
//    if (second != null)
//    {
//        Destroy(allicons[targetX, targetY]);
//    }
//    StartCoroutine(FinishMatch());
//}

//public void SpecialEffect(int targetX, int targetY)
//{
//    GameObject tCurrentIcon = allicons[targetX, targetY];
//    if (tCurrentIcon != null)
//    {
//        if (tCurrentIcon.GetComponent<Icon>().STag == "6Bomb")
//        {
//            //Destroy(allicons[targetX,targetY]);
//            int direction = Random.Range(0, 2);
//            if (direction == 1)
//            {
//                DestroyRow(targetX, targetY);
//            }
//            else if (direction == 0)
//            {
//                DestroyCollum(targetX, targetY);
//            }
//        }
//        else if (tCurrentIcon.GetComponent<Icon>().STag == "7Bomb")
//        {
//            //Destroy(allicons[targetX, targetY]);
//            DestroyArea(targetX, targetY, 1);
//        }
//        else if (tCurrentIcon.GetComponent<Icon>().STag == "8BombBlue")
//        {
//            Destroy(allicons[targetX, targetY]);
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "BluePotion")
//                    {
//                        Destroy(allicons[i, j]);
//                    }
//                }
//            }
//        }
//        else if (tCurrentIcon.GetComponent<Icon>().STag == "8BombGreen")
//        {
//            Destroy(allicons[targetX, targetY]);
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "GreenPotion")
//                    {
//                        Destroy(allicons[i, j]);
//                    }
//                }
//            }
//        }
//        else if (tCurrentIcon.GetComponent<Icon>().STag == "8BombRed")
//        {
//            Destroy(allicons[targetX, targetY]);
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "RedPotion")
//                    {
//                        Destroy(allicons[i, j]);
//                    }
//                }
//            }
//        }
//        else if (tCurrentIcon.GetComponent<Icon>().STag == "8BombYellow")
//        {
//            Destroy(allicons[targetX, targetY]);
//            for (int i = 0; i < width; i++)
//            {
//                for (int j = 0; j < height; j++)
//                {
//                    if (allicons[i, j].GetComponent<Icon>().STag == "YellowPotion")
//                    {
//                        Destroy(allicons[i, j]);
//                    }
//                }
//            }
//        }
//    }
//}

