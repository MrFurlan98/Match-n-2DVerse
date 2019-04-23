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

    private void Awake()
    {
        m_Instance = this;
    }

    public void AddPoint(int pPoint)
    {
        Points += pPoint;
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
}
