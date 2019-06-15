using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    private static ScoreManager m_Instance;

    private int m_Points;

    [SerializeField]
    private int m_GoalPoints;

    [SerializeField]
    private int m_MovesLeft;

    [SerializeField]
    private int m_TargetLeft;

    [SerializeField]
    private List<int> m_NumberToDestroy;

    private string m_Scenario;

    private string m_Type;

    private bool stop;

    private float timer=0;
    private float currentTime=0;

    private bool m_StartGame = false;

    private void Start()
    {
        int tCurrentLevelIndex = LevelManager.Instance.CurrentLevel;

        Debug.Log(LevelManager.Instance.Levels[tCurrentLevelIndex].Scenario);
        Debug.Log(LevelManager.Instance.Levels[tCurrentLevelIndex].Type);

        PopUpUI.Instance.ClosePopUp();

        Scenario = LevelManager.Instance.Levels[tCurrentLevelIndex].Scenario;
        Type = LevelManager.Instance.Levels[tCurrentLevelIndex].Type;
        StartGame = false;
        MovesLeft = LevelManager.Instance.Levels[tCurrentLevelIndex].MovesLeft;
        TargetLeft = LevelManager.Instance.Levels[tCurrentLevelIndex].TargetLeft;
        GoalPoints = LevelManager.Instance.Levels[tCurrentLevelIndex].GoalPoints;
        Points = 0;

        Timer = LevelManager.Instance.Levels[tCurrentLevelIndex].Timer;
        if (LevelManager.Instance.Levels[tCurrentLevelIndex].Type == "Desativar_Bomba")
        {
            SetTargets(TargetLeft);
            Stop = false;
            StartClock();
        }

        GamePlayManager.Instance.BoardReference.ClearBoard();

        GamePlayManager.Instance.BoardReference.InitBoard(tCurrentLevelIndex);

        GamePlayUI.instance.SetBackground();

        //GamePlayUI.instance.SetBackground(BoardManager.Instance.Nivel[i]);

        if (LevelManager.Instance.Levels[tCurrentLevelIndex].Type == "Resgate")
        {
            GamePlayUI.instance.OpenPanel(GamePlayUI.PANELS.RESCUE);
        }
        if (LevelManager.Instance.Levels[tCurrentLevelIndex].Type == "Desativar_Bomba")
        {
            GamePlayUI.instance.OpenPanel(GamePlayUI.PANELS.DEACTIVATE_BOMB);
        }
        if (LevelManager.Instance.Levels[tCurrentLevelIndex].Type == "Sobre_O_Olhar_Da_Gorgona")
        {
            GamePlayUI.instance.OpenPanel(GamePlayUI.PANELS.UNDER_THE_GORGONAS_EYES);
        }
        if (LevelManager.Instance.Levels[tCurrentLevelIndex].Type == "Um_Dos_Doze_Trabalhos")
        {
            GamePlayUI.instance.OpenPanel(GamePlayUI.PANELS.ONE_OF_TWELVE_WORKS);
        }
        ScreenManager.Instance.CloseScreen(ScreenManager.SCREEN.ROADMAP);
        StartGame = true;
        GamePlayManager.Instance.BoardReference.StartCoroutine(GamePlayManager.Instance.BoardReference.StartDalay());
    }
    private void OnEnable()
    {
        StartGame = false;

    }
    private void Awake()
    {
        m_Instance = this;
        Timer = 0;
        MovesLeft = 1;
        StartGame = false;
    }
    public void StopTime(float pTimer)
    {
        Timer = pTimer;
        currentTime = 0;
        stop = true;
    }
    public void StartClock()
    {
        ClockDelay();
        float StartTime = Timer;
        currentTime = Timer;
    }
    public IEnumerator ClockDelay()
    {
        yield return new WaitForSeconds(2);
    }
    private void Update()
    {
        if(!Stop)
        {
            if (currentTime > 0)
            {
                currentTime -= 1 * Time.deltaTime;
                Timer = currentTime;
            }
            else
            {
                StopTime(0);
            }
        }
        
    }
    public void SetTargets(int qtd)
    {
        if(qtd ==5)
        {
            qtd = 4;
        }
        for (int i = 0; i < qtd; i++)
        {
            NumberToDestroy[i] = 5;
        }
        for (int i = qtd; i < NumberToDestroy.Count; i++)
        {
            NumberToDestroy[i] = 0;
        }
    }  
    public void AddPoint(int pPoint)
    {
        Points += pPoint;
    }

    public void ReduceNumberTarget(int pNumber,int indice)
    {
        NumberToDestroy[indice] -= pNumber;
    }

    public static ScoreManager Instance
    {
        get
        {
            return m_Instance;
        }

        set
        {
            m_Instance = value;
        }
    }

    public int Points
    {
        get
        {
            return m_Points;
        }

        set
        {
            m_Points = value;
        }
    }

    public int GoalPoints
    {
        get
        {
            return m_GoalPoints;
        }

        set
        {
            m_GoalPoints = value;
        }
    }

    public int MovesLeft
    {
        get
        {
            return m_MovesLeft;
        }

        set
        {
           
            m_MovesLeft = value;
        }
    }

    public int TargetLeft
    {
        get
        {
            return m_TargetLeft;
        }

        set
        {
            m_TargetLeft = value;
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

    public float Timer
    {
        get
        {
            return timer;
        }

        set
        {
            timer = value;
        }
    }

    public bool StartGame
    {
        get
        {
            return m_StartGame;
        }

        set
        {
            m_StartGame = value;
        }
    }

    public List<int> NumberToDestroy
    {
        get
        {
            return m_NumberToDestroy;
        }

        set
        {
            m_NumberToDestroy = value;
        }
    }

    public bool Stop
    {
        get
        {
            return stop;
        }

        set
        {
            stop = value;
        }
    }
}
