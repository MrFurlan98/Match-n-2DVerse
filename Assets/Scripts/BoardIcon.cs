using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardIcon : MonoBehaviour {
  

    public enum E_State {
        STAND_BY,
        MARK_TO_DESTROY,
        CANT_DESTROY,
        SHADOW,
        PETRIFIED
    }
    public enum E_Type
    {
        NORMAL,
        SPECIAL
    }
    [SerializeField]
    private E_State m_StateIcon = E_State.STAND_BY;
    
    [SerializeField]
    private E_Type m_Type = E_Type.NORMAL;

    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    public float distance = 0;
    public int colunm;
    public int row;
    public int previousColunm;
    public int previousRow;
    private Board board;
    public int targetX;
    public int targetY;
    public bool isMatch = false;
    public bool isIndestructable = false;
    public bool isSpecial = false;
    public int shadowy = 0;

    [SerializeField]
    public string m_sTag;

    private Vector2 tempPosition;

    [SerializeField]
    private List<Icon.Action> m_Actions;

	private SpriteRenderer m_SpriteRenderer;

    private int durability;

    private string theme;

    public void SetBoardData(Icon pIcon)
    {
        STag = pIcon.Tag;

        Actions = pIcon.Actions;

        SpriteRenderer.sprite = pIcon.IconSprite;

        Durability = pIcon.Durability;

        Theme = pIcon.Theme;
    }
    public void SetShadow(Sprite shadow)
    {
        SpriteRenderer.sprite = shadow;
    }
    // Use this for initialization
    void Start () {
        board = FindObjectOfType<Board>();
        previousRow = row;
        previousColunm = colunm;
    }

    void Update()
    {
        //if(isMatch)
        //{
        //    board.FindMatch(targetX, targetY);
        //}
        //if(isMatch && distance >=0.5)
        //{
        //    StartCoroutine(board.FinishMatch());
        //    distance = 0;
        //}


        targetY = colunm; //+ board.offSet;
        targetX = row;
        if(Mathf.Abs(targetX-transform.position.x - board.OffSet) >.1)
        {
            tempPosition = new Vector2(targetX, transform.position.y + board.OffSet);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
            //if(board.allicons[row,colunm]!=this.gameObject)
            //{
            //    board.allicons[row, colunm] = this.gameObject;
            //}
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y + board.OffSet);
            transform.position = tempPosition;
            //board.allicons[row, colunm] = this.gameObject;
        }
        if (Mathf.Abs(targetY - transform.position.y - board.OffSet) > .1)
        {
            tempPosition = new Vector2(transform.position.x,targetY + board.OffSet);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
            //if(board.allicons[row, colunm] != this.gameObject)
            //{
            //    board.allicons[row, colunm] = this.gameObject;
            //}
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY + board.OffSet);
            transform.position = tempPosition;
            //board.allicons[row, colunm] = this.gameObject;
        }
    }

    //private void OnMouseDown()
    //{
    //    if(board.m_CurrentState == GameState.RUNNING)
    //    {
    //        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
    //        {
    //            if (!isSpecial)
    //            {
    //                isMatch = true;
    //            }
    //            board.currentX = targetX;
    //            board.currentY = targetY;
    //        }
    //    } 
    //}

    //private void OnMouseUp()
    //{
    
    //    finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //   // swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
    //    distance = Vector2.Distance(firstTouchPosition, finalTouchPosition);
  
    //    if (distance < 0.5)
    //    {
    //        if (isSpecial)
    //        {
    //            board.SpecialEffect(targetX, targetY);
    //        }
    //        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
    //        {
    //            board.DestroyMatch();
    //        }
    //    }
    //    else
    //    {
    //        if (board.m_CurrentState == GameState.RUNNING)
    //        {
    //            board.m_CurrentState = GameState.STANDBY;
    //           // SwapIcons();
    //        }
    //    }
        
      
    //}

    public IEnumerator CheckCombo()
    {
        yield return new WaitForSeconds(.2f);
        //if(swapingIcon!=null)
        //{
        //    if(!isSpecial || !swapingIcon.GetComponent<Icon>().isSpecial)
        //    {
        //        swapingIcon.GetComponent<Icon>().row = swapingIcon.GetComponent<Icon>().previousRow;
        //        swapingIcon.GetComponent<Icon>().colunm = swapingIcon.GetComponent<Icon>().previousColunm;

        //        row = previousRow;
        //        colunm = previousColunm;
      
        //    }
        //    if(isSpecial && swapingIcon.GetComponent<Icon>().isSpecial)
        //    {
        //       board.m_CurrentState = GameState.onMatch;
        //       board.DoCombo(swapingIcon.GetComponent<Icon>().row, swapingIcon.GetComponent<Icon>().colunm,targetX,targetY);
        //    }
        //}
        //yield return new WaitUntil(() => board.m_CurrentState != GameState.onMatch);
        //board.m_CurrentState = GameState.RUNNING;
    }
    private void OnDestroy()
    {
        IBoardAction[] tActions = GetComponentsInChildren<IBoardAction>();
        if(tActions != null)
        {
            foreach(IBoardAction tAction in tActions)
            {
                // added to queue of actions to trigger when match has finish
           //    GameManager.Instance.PBoard.Actions.Add(delegate { tAction.Invoke(targetX, targetY); });
            }
        }
    }

    public string STag
    {
        get
        {
            return m_sTag;
        }

        set
        {
            m_sTag = value;
        }
    }

    public E_State StateIcon
    {
        get
        {
            return m_StateIcon;
        }

        set
        {
            m_StateIcon = value;
        }
    }

    public E_Type Type
    {
        get
        {
            return m_Type;
        }

        set
        {
            m_Type = value;
        }
    }

    public SpriteRenderer SpriteRenderer
    {
        get
        {
            return m_SpriteRenderer;
        }

        set
        {
            m_SpriteRenderer = value;
        }
    }

    public List<Icon.Action> Actions
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

    public int Durability
    {
        get
        {
            return durability;
        }

        set
        {
            durability = value;
        }
    }

    public string Theme
    {
        get
        {
            return theme;
        }

        set
        {
            theme = value;
        }
    }
}
