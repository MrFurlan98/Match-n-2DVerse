using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardIcon : MonoBehaviour {
  

    public enum E_State {
        STAND_BY,
        MARK_TO_DESTROY,
        CANT_DESTROY,
        OBJECTIVE,
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
    public int petrified = 0;

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
    public void SetSprite(Sprite pSprite)
    {
        SpriteRenderer.sprite = pSprite;
    }
    // Use this for initialization
    private void Awake()
    {
        targetY = colunm; //+ board.offSet;
        targetX = row;
    }
    void Start () {
        board = FindObjectOfType<Board>();
        previousRow = row;
        previousColunm = colunm;
        targetY = colunm; //+ board.offSet;
        targetX = row;
    }

    void Update()
    {
        targetY = colunm; //+ board.offSet;
        targetX = row;
        /*if(Mathf.Abs(targetX-transform.position.x - board.OffSet) >.1)
        {
            tempPosition = new Vector2(targetX, transform.position.y + board.OffSet);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y + board.OffSet);
            transform.position = tempPosition;
        }
        if (Mathf.Abs(targetY - transform.position.y - board.OffSet) > .1)
        {
            tempPosition = new Vector2(transform.position.x,targetY + board.OffSet);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY + board.OffSet);
            transform.position = tempPosition;
        }*/
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
