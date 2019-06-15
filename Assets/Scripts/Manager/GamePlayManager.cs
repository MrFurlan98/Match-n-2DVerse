using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour {
    private static GamePlayManager m_Instance;
    [SerializeField]
    private Board m_BoardReference;

    private void Awake()
    {
        Instance = this;    
    }

    public static GamePlayManager Instance
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

    public Board BoardReference
    {
        get
        {
            return m_BoardReference;
        }

        set
        {
            m_BoardReference = value;
        }
    }
}
