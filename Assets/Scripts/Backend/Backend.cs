using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameSparks.Core;
public partial class Backend: MonoBehaviour {
    private static Backend m_Instance;


    private void Awake()
    {
        Instance = this;
        
    }

    public void DailyLogin(Action<int> pSucessAction)
    {

    }

    public bool ServerAvailable()
    {
        return GS.Available;
    }

    public bool WasAuthenticated()
    {
        return GS.Authenticated;
    }

    public static Backend Instance
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
}
