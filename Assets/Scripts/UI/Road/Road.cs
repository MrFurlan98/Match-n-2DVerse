using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour {

    [SerializeField]
    private LevelButtonUI[] m_Levels;




    public LevelButtonUI[] Levels
    {
        get
        {
            return m_Levels;
        }

        set
        {
            m_Levels = value;
        }
    }
}
