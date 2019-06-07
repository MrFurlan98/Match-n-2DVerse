using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Level{ 

    private int[,] m_Model;

    private string m_Type;

    private string m_Scenario;

    private int m_GoalPoints;

    private int m_MovesLeft;

    private int m_TargetLeft;

    private float timer = 45;

    public int[,] Model
    {
        get
        {
            return m_Model;
        }

        set
        {
            m_Model = value;
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
}
