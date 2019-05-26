using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePlayUI : MonoBehaviour {
   
    #region Avatar
    [SerializeField]
    private Image m_HeadGamePlayAvatar;
    [SerializeField]
    private Image m_ArmGamePlayAvatar;
    [SerializeField]
    private Image m_LegGamePlayAvatar;
    [SerializeField]
    private Image m_BodyGamePlayAvatar;

    #endregion
    public static GamePlayUI instance;

    [SerializeField]
    private Image m_iBackground;

    [SerializeField]
    private Button m_bPause;

    [SerializeField]
    private Text m_tScore;

    [SerializeField]
    private Text m_tMoves;

    [SerializeField]
    private Text m_ObjectivePoint;

    [SerializeField]
    private Board m_pBoard;

    [SerializeField]
    private MonsterAnimation m_pMonsterAnimation;

    bool hasFinished = false;

    private void OnEnable()
    {
        hasFinished = false;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateMenuButton();
    }

    private void Update()
    {
      
        m_tScore.text =  ScoreManager.Instance.Points.ToString();

        m_tMoves.text = ScoreManager.Instance.MovesLeft.ToString();

        m_ObjectivePoint.text = ScoreManager.Instance.GoalPoints.ToString();

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

    public void SetMembers(Image m_HeadButton, Image m_LegsButton, Image m_ArmButton, Image m_BodyButton)
    {
        m_HeadGamePlayAvatar.sprite = m_HeadButton.sprite;
        m_LegGamePlayAvatar.sprite = m_LegsButton.sprite;
        m_ArmGamePlayAvatar.sprite = m_ArmButton.sprite;
        m_BodyGamePlayAvatar.sprite = m_BodyButton.sprite;
    }

}
