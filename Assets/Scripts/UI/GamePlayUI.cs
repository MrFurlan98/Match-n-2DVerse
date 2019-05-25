using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePlayUI : MonoBehaviour {

    [SerializeField]
    private Image m_iBackground;

    [SerializeField]
    private Image m_TargetSprite;

    [SerializeField]
    private Button m_bPause;

    [SerializeField]
    private Text m_tScore;

    [SerializeField]
    private Text m_tMoves;

    [SerializeField]
    private Text m_tModulos;

    [SerializeField]
    private Text m_ObjectivePoint;

    [SerializeField]
    private Board m_pBoard;

    [SerializeField]
    private MonsterAnimation m_pMonsterAnimation;

    private string m_Scenario;

    private string m_Type;


    bool hasFinished = false;

    private void OnEnable()
    {
        hasFinished = false;
        
    }

    private void Start()
    {
        UpdateMenuButton();
    }

    private void Update()
    {
        m_tModulos.text = ScoreManager.Instance.TargetLeft.ToString();
      
        m_tScore.text =  ScoreManager.Instance.Points.ToString();

        m_tMoves.text = ScoreManager.Instance.MovesLeft.ToString();

        m_TargetSprite.sprite = ScoreManager.Instance.IconToDestroy;

        m_ObjectivePoint.text = ScoreManager.Instance.GoalPoints.ToString();

        Scenario = ScoreManager.Instance.Scenario;

        Type = ScoreManager.Instance.Type;


        if (!hasFinished && ScoreManager.Instance.MovesLeft <= 0)
        {
            hasFinished = true;
            PopUpUI.Instance.OpenPopUp("Fim de jogo", ScoreManager.Instance.Points >= ScoreManager.Instance.GoalPoints ? true : false , GamePlayUI.BackMenu);
        }


    }
    public void Switch()
    {
        StopAllCoroutines();
        StartCoroutine(GameManager.Instance.PBoard.SwitchIcons());
        
    }
    public void Upgrade()
    {
        StopAllCoroutines();
        StartCoroutine(GameManager.Instance.PBoard.PowerUp());

    }
    private void UpdateMenuButton()
    {
        m_bPause.onClick = new Button.ButtonClickedEvent();
        m_bPause.onClick.AddListener(BackMenu);
    }

    private void SelectPauseButton()
    {
        UIManager.Instance.OpenScreen(UIManager.SCREEN.PAUSE);
    }

    public void SetBackground(Sprite pBackground)
    {
        m_iBackground.sprite = pBackground;
    }

    public Text TMoves
    {
        get
        {
            return m_tMoves;
        }

        set
        {
            m_tMoves = value;
        }
    }

    public Text TScore
    {
        get
        {
            return m_tScore;
        }

        set
        {
            m_tScore = value;
        }
    }

    public Text TModulos
    {
        get
        {
            return m_tModulos;
        }

        set
        {
            m_tModulos = value;
        }
    }

    public Image TargetSprite
    {
        get
        {
            return m_TargetSprite;
        }

        set
        {
            m_TargetSprite = value;
        }
    }

    public Board PBoard
    {
        get
        {
            return m_pBoard;
        }

        set
        {
            m_pBoard = value;
        }
    }

    public string Scenario
    {
        get
        {
            return m_Scenario;
        }

        set
        {
            m_Scenario = value;
        }
    }

    public string Type
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

    // set all events when has a match
    public void SetEvents()
    {
        m_pMonsterAnimation.TryReviveMonster(true);
    }

    public static void BackMenu()
    {
        UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.ROADMAP);
        UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.GAMEPLAY);
 
    }

}
