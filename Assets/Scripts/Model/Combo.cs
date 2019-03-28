﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour {

    private List<IBoardAction> m_Actions;
    private BoardIcon m_Icon1;
    private BoardIcon m_Icon2;

    public List<IBoardAction> Actions
    {
        get
        {
            return m_Actions;
        }

        set
        {
            m_Actions = value;
        }
    }

    public BoardIcon Icon1
    {
        get
        {
            return m_Icon1;
        }

        set
        {
            m_Icon1 = value;
        }
    }

    public BoardIcon Icon2
    {
        get
        {
            return m_Icon2;
        }

        set
        {
            m_Icon2 = value;
        }
    }


}