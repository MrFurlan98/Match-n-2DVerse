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
    private Text m_qtdZeusText;
    [SerializeField]
    private Text m_qtdUpApText;
    [SerializeField]
    private Text m_qtdUpGregText;
    [SerializeField]
    private Text m_qtdSwitchText;



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

    [SerializeField]
    private Sprite m_BackGround_Apocaliptco;

    [SerializeField]
    private Sprite m_BackGround_Grego;

    public enum PANELS
    {
        RESCUE,
        DEACTIVATE_BOMB,
        UNDER_THE_GORGONAS_EYES,
        ONE_OF_TWELVE_WORKS,
        NONE
    }

    [Header("Panels")]
    [SerializeField]
    private List<UIPanel> m_pPanel = new List<UIPanel>();

    [System.Serializable]
    public class UIPanel
    {
        public GameObject m_pPanelObject;
        public PANELS m_pPanel;
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
        SetBackground();
        //m_tModulos.text = ScoreManager.Instance.TargetLeft.ToString();
      
        //m_tScore.text =  ScoreManager.Instance.Points.ToString();

       // m_tMoves.text = ScoreManager.Instance.MovesLeft.ToString();

        //m_TargetSprite.sprite = ScoreManager.Instance.IconToDestroy;

       // m_ObjectivePoint.text = ScoreManager.Instance.GoalPoints.ToString();

        Scenario = ScoreManager.Instance.Scenario;

        Type = ScoreManager.Instance.Type;

        //Timer.text = ScoreManager.Instance.Timer.ToString("0");
        

    }
    public void ZeusThunder()
    {
        if (InventoryManager.instance.listConsumables[2].qtdItem > 0)
        {
            StopAllCoroutines();
            StartCoroutine(GameManager.Instance.PBoard.ZeusThunder());
            InventoryManager.instance.listConsumables[2].qtdItem--;
            return;
        }
    }
    public void Switch()
    {
        if (InventoryManager.instance.listConsumables[0].qtdItem > 0)
        {
            StopAllCoroutines();
            StartCoroutine(GameManager.Instance.PBoard.SwitchIcons());
            InventoryManager.instance.listConsumables[0].qtdItem--;
            return;
        }
    }
    public void Upgrade()
    {
        if (InventoryManager.instance.listConsumables[1].qtdItem > 0)
        {
            StopAllCoroutines();
            StartCoroutine(GameManager.Instance.PBoard.PowerUp());
            InventoryManager.instance.listConsumables[1].qtdItem--;
            return;
        }
        print("ue");
    }

    public void UpgradeGrego()
    {
        if (InventoryManager.instance.listConsumables[3].qtdItem > 0)
        {
            StopAllCoroutines();
            StartCoroutine(GameManager.Instance.PBoard.PowerUp());
            InventoryManager.instance.listConsumables[3].qtdItem--;
            return;
        }
        print("ue");
    }
    private void UpdateMenuButton()
    {
        m_bPause.onClick = new Button.ButtonClickedEvent();
        m_bPause.onClick.AddListener(BackMenu);
    }
    public void OpenPanel(PANELS pPanel)
    {
        GetPanelObject(pPanel).SetActive(true);
    }
    public void ClosePanel(PANELS pPanel)
    {
        GetPanelObject(pPanel).SetActive(false);
    }
    public GameObject GetPanelObject(PANELS pPanel)
    {
        UIPanel UI = null;
        for (int i = 0; i < m_pPanel.Count; i++)
        {
            if(pPanel == m_pPanel[i].m_pPanel)
            {
                UI = m_pPanel[i];
                break;
            }
        }
        return UI.m_pPanelObject;
    }
    private void SelectPauseButton()
    {
        UIManager.Instance.OpenScreen(UIManager.SCREEN.PAUSE);
    }

    public void SetBackground()
    {
        if (Scenario == "APOCALIPTICO")
        {
            IBackground.sprite = BackGround_Apocaliptco;
            m_qtdUpApText.text = InventoryManager.instance.listConsumables[1].qtdItem + "x"; ;
            m_qtdSwitchText.text = InventoryManager.instance.listConsumables[0].qtdItem + "x"; ;


    //Debug.Log("aqui");
}
        if (Scenario == "GREGO")
        {
            IBackground.sprite = BackGround_Grego;
            m_qtdZeusText.text=InventoryManager.instance.listConsumables[2].qtdItem+"x";
            m_qtdUpGregText.text= InventoryManager.instance.listConsumables[3].qtdItem + "x";
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

    public Image IBackground
    {
        get
        {
            return m_iBackground;
        }

        set
        {
            m_iBackground = value;
        }
    }

    public Sprite BackGround_Apocaliptco
    {
        get
        {
            return m_BackGround_Apocaliptco;
        }

        set
        {
            m_BackGround_Apocaliptco = value;
        }
    }

    public Sprite BackGround_Grego
    {
        get
        {
            return m_BackGround_Grego;
        }

        set
        {
            m_BackGround_Grego = value;
        }
    }



    // set all events when has a match
    public void SetEvents()
    {
        m_pMonsterAnimation.TryReviveMonster(true);
    }

    public void BackMenu()
    {
        UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.ROADMAP);
        UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.GAMEPLAY);
        UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.POPUP);
        ClosePanel(PANELS.RESCUE);
        ClosePanel(PANELS.DEACTIVATE_BOMB);
        ClosePanel(PANELS.UNDER_THE_GORGONAS_EYES);
        ClosePanel(PANELS.ONE_OF_TWELVE_WORKS);
    }

    public void SetMembers(Image m_HeadButton, Image m_LegsButton, Image m_ArmButton, Image m_BodyButton)
    {
        m_HeadGamePlayAvatar.sprite = m_HeadButton.sprite;
        m_LegGamePlayAvatar.sprite = m_LegsButton.sprite;
        m_ArmGamePlayAvatar.sprite = m_ArmButton.sprite;
        m_BodyGamePlayAvatar.sprite = m_BodyButton.sprite;
    }

}
