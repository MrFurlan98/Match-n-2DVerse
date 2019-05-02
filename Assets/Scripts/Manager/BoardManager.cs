using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    private static BoardManager m_Instance;

    [SerializeField]
    private Board m_pBoard;

    [SerializeField]
    private int m_Level;

    private void Awake()
    {
        Instance = this;
    }

    #region Methods

    public void GenerateBoard()
    {

    }

    public static BoardManager Instance
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

    public int Level
    {
        get
        {
            return m_Level;
        }

        set
        {
            m_Level = value;
        }
    }

    #endregion
}
