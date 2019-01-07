using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour {

    public int colunm;
    public int row;
    private Board board;
    public int targetX;
    public int targetY;
    public bool isMatch = false;
    public bool isIndestructable = false;
    [SerializeField]
    public string m_sTag;
    private Vector2 tempPosition;

	private SpriteRenderer theSpriteRenderer;

    // Use this for initialization
    void Start () {
        board = FindObjectOfType<Board>();
        /*targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetX;
        colunm = targetY;*/
		theSpriteRenderer = GetComponent<SpriteRenderer> ();
    }

    void Update()
    {
        if(isMatch)
        {
            board.FindMatch(targetX, targetY);
        }
        targetY = colunm; //+ board.offSet;
        targetX = row;
        if(Mathf.Abs(targetY-transform.position.y - board.offSet) >.1)
        {
            tempPosition = new Vector2(transform.position.x, targetY + board.offSet);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
            if(board.allicons[row,colunm]!=this.gameObject)
            {
                board.allicons[row, colunm] = this.gameObject;
            }
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY + board.offSet);
            transform.position = tempPosition;
            
        }
    }

    private void OnMouseDown()
    {
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
        {
            isMatch = true;
            board.currentX = targetX;
            board.currentY = targetY;
        }
    }

    private void OnMouseUp()
    {
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
        {
            board.DestroyMatch();
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
