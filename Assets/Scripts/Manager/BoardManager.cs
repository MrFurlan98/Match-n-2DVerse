using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    private static BoardManager m_Instance;

    [SerializeField]
    private Board m_pBoard;

    [SerializeField]
    private List<Level> m_levels;

    private void Awake()
    {
        Instance = this;
    }

    #region Methods


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

    public List<Level> Levels
    {
        get
        {
            return m_levels;
        }

        set
        {
            m_levels = value;
        }
    }

    #endregion
}
