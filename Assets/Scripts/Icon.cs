using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour {

    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    public float swipeAngle = 0;
    public float distance = 0;
    public int colunm;
    public int row;
    public int previousColunm;
    public int previousRow;
    public int swapX;
    public int swapY;
    private Board board;
    private GameObject swapingIcon;
    public int targetX;
    public int targetY;
    public bool isMatch = false;
    public bool isIndestructable = false;
    public bool isSpecial = false;
    [SerializeField]
    public string m_sTag;
    private Vector2 tempPosition;

	private SpriteRenderer theSpriteRenderer;

    // Use this for initialization
    void Start () {
        board = FindObjectOfType<Board>();
		theSpriteRenderer = GetComponent<SpriteRenderer> ();
        previousRow = row;
        previousColunm = colunm;
    }

    void Update()
    {
        if(isMatch)
        {
            board.FindMatch(targetX, targetY);
        }
        if(isMatch && distance >=0.5)
        {
            StartCoroutine(board.FinishMatch());
            distance = 0;
        }
        targetY = colunm; //+ board.offSet;
        targetX = row;
        if(Mathf.Abs(targetX-transform.position.x - board.offSet) >.1)
        {
            tempPosition = new Vector2(targetX, transform.position.y + board.offSet);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
            if(board.allicons[row,colunm]!=this.gameObject)
            {
                board.allicons[row, colunm] = this.gameObject;
            }
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y + board.offSet);
            transform.position = tempPosition;
            board.allicons[row, colunm] = this.gameObject;
        }
        if (Mathf.Abs(targetY - transform.position.y - board.offSet) > .1)
        {
            tempPosition = new Vector2(transform.position.x,targetY + board.offSet);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
            if(board.allicons[row, colunm] != this.gameObject)
            {
                board.allicons[row, colunm] = this.gameObject;
            }
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY + board.offSet);
            transform.position = tempPosition;
            board.allicons[row, colunm] = this.gameObject;
        }
    }

    private void OnMouseDown()
    {
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
        {
            if(!isSpecial)
            {
                isMatch = true;
            }
            board.currentX = targetX;
            board.currentY = targetY;
        }
    }

    private void OnMouseUp()
    {
        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180/Mathf.PI;
        distance = Vector2.Distance(firstTouchPosition, finalTouchPosition);
        Debug.Log(distance);
        if(distance<0.5)
        {
            if(isSpecial)
            {
                board.SpecialEffect(targetX, targetY);
            }
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
            {
                board.DestroyMatch();
            }
        }
        else
        {
            SwapIcons();
        }
    }

    public void SwapIcons()
    {
        if(swipeAngle >-45 && swipeAngle <= 45 && row < board.width-1)//right swap
        {
            swapingIcon = board.allicons[row + 1,colunm];
            swapingIcon.GetComponent<Icon>().row -= 1;
            row += 1;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && colunm < board.height-1)//up swap
        {
            swapingIcon = board.allicons[row, colunm +1];
            swapingIcon.GetComponent<Icon>().colunm -= 1;
            colunm += 1;
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && row > 0)//left swap
        {
            swapingIcon = board.allicons[row -1, colunm];
            swapingIcon.GetComponent<Icon>().row += 1;
            row -= 1;
        }
        else if(swipeAngle < -45 && swipeAngle >= -135 && colunm > 0)//down swap
        {
            swapingIcon = board.allicons[row, colunm-1];
            swapingIcon.GetComponent<Icon>().colunm += 1;
            colunm -= 1;
        }
        StartCoroutine(CheckCombo());
    }

    public IEnumerator CheckCombo()
    {
        yield return new WaitForSeconds(.4f);
        if(swapingIcon!=null)
        {
            if(!isSpecial || !swapingIcon.GetComponent<Icon>().isSpecial)
            {
                swapingIcon.GetComponent<Icon>().row = row;
                swapingIcon.GetComponent<Icon>().colunm = colunm;
                row = previousRow;
                colunm = previousColunm;
            }
            if(isSpecial && swapingIcon.GetComponent<Icon>().isSpecial)
            {
                board.DoCombo(swapingIcon.GetComponent<Icon>().row, swapingIcon.GetComponent<Icon>().colunm,targetX,targetY);
            }
        }
    }
    private void OnDestroy()
    {
        IDestroyAction[] tActions = GetComponentsInChildren<IDestroyAction>();
        if(tActions != null)
        {
            foreach(IDestroyAction tAction in tActions)
            {
                // added to queue of actions to trigger when match has finish
               GameManager.Instance.PBoard.m_pActions.Add(delegate { tAction.Invoke(targetX, targetY); });
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

}
