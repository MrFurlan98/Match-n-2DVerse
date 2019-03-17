﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    STANDBY,
    RUNNING,
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

public class Board : MonoBehaviour {
    [System.Serializable]
    public class SpecialIcon
    {
        [SerializeField]
        private GameObject m_IconPrefab;

        [SerializeField]
        private int m_MatchValue;

        public GameObject IconPrefab
        {
            get
            {
                return m_IconPrefab;
            }

            set
            {
                m_IconPrefab = value;
            }
        }

        public int MatchValue
        {
            get
            {
                return m_MatchValue;
            }

            set
            {
                m_MatchValue = value;
            }
        }
        
    }

    private GameState m_CurrentState = GameState.STANDBY;

    [SerializeField]
    private GameObject[] m_PrefabIcons = new GameObject[0];

    [SerializeField]
    private SpecialIcon[] m_SpecialIcons = new SpecialIcon[0];

    private Icon[,] m_Icons;

    private System.Action m_TriggerMatch;
    // create a queue to do actions before the next match 
    [SerializeField]
    private List<System.Action> m_Actions = new List<System.Action>();

    [SerializeField]
    private List<System.Action> m_BoardModifies = new List<System.Action>();

    private Vector2 m_InitPosition;

    [Header("Config Board Game")]

    [SerializeField]
    private int m_Heigth;

    [SerializeField]
    private int m_Width;

    [Range(0, 3)]
    [SerializeField]
    private float m_RefillDelay;

    [Range(0, 3)]
    [SerializeField]
    private float m_SwapDelay;

    [SerializeField]
    private int m_OffSet = 0;

    // Use this for initialization
    void Start() {
        m_Icons = new Icon[m_Width, m_Heigth];
        m_CurrentState = GameState.STANDBY;
        //UpdateMoveScore();
        InitBoard();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < m_Icons.GetLength(0); i++)
            {
                for (int j = 0; j < m_Icons.GetLength(1); j++)
                {
                    if(m_Icons[i, j] != null)
                        Destroy(m_Icons[i, j].gameObject);
                    m_Icons[i, j] = null;
                    
                }
            }
            Start();
        }

        if (m_CurrentState == GameState.RUNNING)
            return;

        if (OnMouseDown())
        {
            m_InitPosition = MousePosition();
        }
        if (OnMouseUp())
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

    private void InitBoard()
    {
        for (int i = 0; i < m_Width; i++)
        {
            for (int j = 0; j < m_Heigth; j++)
            {
                Icon tIconItem = GenerateRandomIcon(i, j);

                tIconItem.colunm = j;
                tIconItem.row = i;

                tIconItem.transform.parent = this.transform;

                m_Icons[i, j] = tIconItem;
            }
        }
    }

    private IEnumerator BoardRunning(Vector2Int pOriginIndex, E_Direction pDirection)
    {
        m_CurrentState = GameState.RUNNING;

        int tOriginMatchAmount = 0;

        int tToMatchAmount = 0;

        Vector2Int pToIndex = new Vector2Int(-1,-1);

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

                yield return SwapRoutine(pOriginIndex, pToIndex);

                tOriginMatchAmount = CountIconsInMatch(pOriginIndex);

                tToMatchAmount = CountIconsInMatch(pToIndex);

                break;
            case E_Direction.DOWN:

                pToIndex = new Vector2Int(pOriginIndex.x, pOriginIndex.y - 1);

                yield return SwapRoutine(pOriginIndex, pToIndex);

                tOriginMatchAmount = CountIconsInMatch(pOriginIndex);

                tToMatchAmount = CountIconsInMatch(pToIndex);

                break;
            case E_Direction.UP:

                pToIndex = new Vector2Int(pOriginIndex.x, pOriginIndex.y + 1);

                yield return SwapRoutine(pOriginIndex, pToIndex);

                tOriginMatchAmount = CountIconsInMatch(pOriginIndex);

                tToMatchAmount = CountIconsInMatch(pToIndex);

                break;
            case E_Direction.RIGHT:

                pToIndex = new Vector2Int(pOriginIndex.x + 1, pOriginIndex.y);

                yield return SwapRoutine(pOriginIndex, pToIndex);

                tOriginMatchAmount = CountIconsInMatch(pOriginIndex);

                tToMatchAmount = CountIconsInMatch(pToIndex);

                break;
        }

        yield return RunSpecialActions();

        // second state resolve tags 
        // first action of second state destroy all icons (with tag MARK_TO_DESTROY)
        DestroyIcons();

        GenerateSpecialIcon(pOriginIndex, pToIndex, tOriginMatchAmount, tToMatchAmount);

        // the last state is refill board 
        DropCollumns();

        // add time to collumn drop
        yield return new WaitForSeconds(m_RefillDelay);

        RefillBoard();

        m_InitPosition = -Vector2.one;

        m_CurrentState = GameState.STANDBY;
    }

    #region Board Actions

    IEnumerator RunSpecialActions()
    {
        for (int i = 0; i < m_BoardModifies.Count; i++)
        {
            m_BoardModifies[i].Invoke();
            yield return new WaitForEndOfFrame();
        }
        m_BoardModifies.Clear();
    }

    void GenerateSpecialIcon(Vector2Int pOriginIndex, Vector2Int pToIndex, int pOriginAmount, int pToAmount)
    {
        List<SpecialIcon> tSpecialOriginIcons = new List<SpecialIcon>();

        List<SpecialIcon> tSpecialToIcons = new List<SpecialIcon>();

        if (pOriginAmount >= 3)
        {
            for (int i = 0; i < m_SpecialIcons.Length; i++)
            {
                if(pOriginAmount == m_SpecialIcons[i].MatchValue)
                {
                    tSpecialOriginIcons.Add(m_SpecialIcons[i]);
                }
            }

            int tOriginRandonIndex = UnityEngine.Random.Range(0, tSpecialOriginIcons.Count);

            if(tSpecialOriginIcons.Count > 0)
                m_Icons[pOriginIndex.x, pOriginIndex.y] = GenerateIcon(pOriginIndex.x, pOriginIndex.y, tSpecialOriginIcons[tOriginRandonIndex].IconPrefab);
        }
        if(pToAmount >= 3)
        {
            for (int i = 0; i < m_SpecialIcons.Length; i++)
            {
                if (pToAmount == m_SpecialIcons[i].MatchValue)
                {
                    tSpecialToIcons.Add(m_SpecialIcons[i]);
                }
            }

            int tToRandonIndex = UnityEngine.Random.Range(0, tSpecialToIcons.Count);

            if (tSpecialToIcons.Count > 0)
                m_Icons[pToIndex.x, pToIndex.y] = GenerateIcon(pToIndex.x, pToIndex.y, tSpecialToIcons[tToRandonIndex].IconPrefab);
        }
    }

    void BoardStay(Vector2Int pIconIndex)
    {
        List<Vector2Int> tIcons = null;

        GetRegionIcons(pIconIndex.x, pIconIndex.y, ref tIcons);

        //Debug.Log(tIcons.Count);

        if (IsAMatch(tIcons))
        {
            MarkToDestroy(tIcons);
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

        for (int i = pY; i < m_Heigth - 1; i++)
        {
            if (m_Icons[pX, i] == null)
                tDropCount++;
            else
                break;
            
        }

        for (int i = pY; i < m_Heigth - tDropCount; i++)
        {

            int tAboveIndex = i + tDropCount;

            Icon tIcon = m_Icons[pX, i];

            m_Icons[pX, i] = m_Icons[pX, tAboveIndex];

            m_Icons[pX, tAboveIndex] = tIcon;

            if (m_Icons[pX, i] != null)
            {
                m_Icons[pX, i].row = pX;
                m_Icons[pX, i].colunm = tAboveIndex - tDropCount;
            }
        }
    }

    void DestroyIcons()
    {
        for (int i = 0; i < m_Icons.GetLength(0); i++)
        {
            for (int j = 0; j < m_Icons.GetLength(1); j++)
            {
                if (m_Icons[i, j].StateIcon == Icon.E_State.MARK_TO_DESTROY)
                {
                    Destroy(m_Icons[i, j].gameObject);
                    m_Icons[i, j] = null;
                }
            }
        }
    }

    private void RefillBoard()
    {
        for (int i = 0; i < m_Width; i++)
        {
            for (int j = 0; j < m_Heigth; j++)
            {
                if (m_Icons[i, j] == null)
                {
                    Icon tIcon = GenerateRandomIcon(i, j);
                    tIcon.colunm = j;
                    tIcon.row = i;
                    tIcon.transform.parent = transform;
                    m_Icons[i, j] = tIcon;
                }
                //else
                //{
                //    allicons[i, j].GetComponent<Icon>().previousColunm = j;
                //    allicons[i, j].GetComponent<Icon>().previousRow = i;
                //}
            }
        }
    }

    private IEnumerator SwapRoutine(Vector2Int pFrom, Vector2Int pTo)
    {
        if (IsInBoardRange(pTo.x, pTo.y))
        {
            SwapIcons(pFrom, pTo);

            yield return new WaitForSeconds(m_SwapDelay);

            List<Vector2Int> tFromIcons = null;

            GetRegionIcons(pFrom.x, pFrom.y, ref tFromIcons);

            List<Vector2Int> tToIcons = null;

            GetRegionIcons(pTo.x, pTo.y, ref tToIcons);

            bool tReSwap = true;

            if (IsAMatch(tFromIcons))
            {
                MarkToDestroy(tFromIcons);
                tReSwap = false;
            }

            if (IsAMatch(tToIcons))
            {
                MarkToDestroy(tToIcons);
                tReSwap = false;
            }

            if (tReSwap)
            {
                SwapIcons(pTo, pFrom);
            }
        }
    }

    private void SwapIcons(Vector2Int pFrom, Vector2Int pTo)
    {
        m_Icons[pFrom.x, pFrom.y].row = pTo.x;
        m_Icons[pFrom.x, pFrom.y].colunm = pTo.y;

        m_Icons[pTo.x, pTo.y].row = pFrom.x;
        m_Icons[pTo.x, pTo.y].colunm = pFrom.y;

        Icon tIcon = m_Icons[pFrom.x, pFrom.y];

        m_Icons[pFrom.x, pFrom.y] = m_Icons[pTo.x, pTo.y];

        m_Icons[pTo.x, pTo.y] = tIcon;
    }

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
            Icon tIcon = m_Icons[tIndex.x, tIndex.y];

            if (tIcon.StateIcon != Icon.E_State.CANT_DESTROY)
            {
                IBoardAction tBoardAction = tIcon.GetComponent<IBoardAction>();
                if (tBoardAction != null)
                {
                    int tX = tIndex.x;
                    int tY = tIndex.y;
                    m_BoardModifies.Add(delegate {
                        tBoardAction.Action(tX, tY, m_Icons);
                    });
                }
                tIcon.StateIcon = Icon.E_State.MARK_TO_DESTROY;
            }
        }
    }

    Icon GenerateRandomIcon(int pX, int pY)
    {
        int tRandoIndex = UnityEngine.Random.Range(0, 4);

        GameObject tNewIcon = Instantiate(m_PrefabIcons[tRandoIndex], new Vector2(pX, pY), Quaternion.identity);
        return tNewIcon.GetComponent<Icon>();
    }

    Icon GenerateIcon(int pX, int pY, GameObject pIconPrefab)
    {
        GameObject tNewIcon = Instantiate(pIconPrefab, new Vector2(pX, pY), Quaternion.identity);
        Icon tIcon = tNewIcon.GetComponent<Icon>();
        tIcon.row = pX;
        tIcon.colunm = pY;
        return tIcon;
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

        Icon tIcon = m_Icons[pX, pY];

        for (int i = pX - 1; i <= pX + 1; i++)
        {
            if(IsInBoardRange(i, pY) && i != pX && m_Icons[ i, pY].STag == tIcon.STag && !pIcons.Contains(new Vector2Int(i, pY)))
            {
                GetRegionIcons(i, pY, ref pIcons) ;
            
            }
        }

        for (int j = pY - 1; j <= pY + 1; j++)
        {
            if (IsInBoardRange(pX, j) && j != pY  && m_Icons[pX, j].STag == tIcon.STag && !pIcons.Contains(new Vector2Int(pX, j)))
            {
                
                GetRegionIcons(pX, j, ref pIcons);
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
                tCurrentPosition.y *= (j + OffSet - 1);
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
        return pX >= 0 && pX < m_Width && pY >= 0 && pY < m_Heigth;
    }

    bool IsAMatch(List<Vector2Int> pIcons)
    {
        return pIcons.Count >= 3;
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

