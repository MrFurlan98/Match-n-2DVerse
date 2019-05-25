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
    private int m_TargetLeft =0;

    [SerializeField]
    private Sprite m_IconToDestroy;

    private string m_Scenario;

    private string m_Type;

    private void Awake()
    {
        m_Instance = this;
    }

    public void AddPoint(int pPoint)
    {
        Points += pPoint;
    }

    public void ReduceNumberTarget(int pNumber)
    {

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

    public Sprite IconToDestroy
    {
        get
        {
            return m_IconToDestroy;
        }

        set
        {
            m_IconToDestroy = value;
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
}
