using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PlayFab.Events;
using PlayFab.ClientModels;
using PlayFab;
public partial class Backend: MonoBehaviour {
    private static Backend m_Instance;

    private static string GUEST_KEY= "GUEST_ID";
    private static string GUEST_USERNAME = "GUEST_USERNAME";

    private void Awake()
    {
        Instance = this;
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
