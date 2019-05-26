using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePlayUI : MonoBehaviour {




    [SerializeField]
    private Image m_iBackground;

    /*[SerializeField]
    private Image m_TargetSprite;

    

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

    [SerializeField]*/

    [SerializeField]
    private Button m_bPause;
    private Text m_timer;

    [SerializeField]
    private MonsterAnimation m_pMonsterAnimation;

    private string m_Scenario;

    private string m_Type;

    private void Start()
    {

        UpdateMenuButton();
    }

    private void Update()
    {
        //m_tModulos.text = ScoreManager.Instance.TargetLeft.ToString();
      
        //m_tScore.text =  ScoreManager.Instance.Points.ToString();

       // m_tMoves.text = ScoreManager.Instance.MovesLeft.ToString();

        //m_TargetSprite.sprite = ScoreManager.Instance.IconToDestroy;

       // m_ObjectivePoint.text = ScoreManager.Instance.GoalPoints.ToString();

        //Scenario = ScoreManager.Instance.Scenario;

        //Type = ScoreManager.Instance.Type;

        //Timer.text = ScoreManager.Instance.Timer.ToString("0");
        

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

    public Text Timer
    {
        get
        {
            return m_timer;
        }

        set
        {
            m_timer = value;
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
        UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.POPUP);
        UIManagerBeta.Instance.ClosePanel(UIManagerBeta.PANELS.RESCUE);
        UIManagerBeta.Instance.ClosePanel(UIManagerBeta.PANELS.DEACTIVATE_BOMB);
        UIManagerBeta.Instance.ClosePanel(UIManagerBeta.PANELS.UNDER_THE_GORGONAS_EYES);
    }

}
