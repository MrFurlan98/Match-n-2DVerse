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
        StartCoroutine(VerifyServerConnection());
    }

    IEnumerator VerifyServerConnection()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(2);
            if (GS.Available)
            {
                //UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.LOADING);
                //UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.AUTHETICATION);
                UIManager.Instance.CloseScreen<LoadingUI>();
                UIManager.Instance.OpenScreen<AuthenticationUI>();
                break;
            }
        }
    }

    public void DailyLogin(Action<int> pSucessAction)
    {

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
