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
        StartGame = false;
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
    public List<int> SetTargets(int qtd)
    {
        qtd = BoardManager.Instance.GetQtd(qtd);
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
        return NumberToDestroy;
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
