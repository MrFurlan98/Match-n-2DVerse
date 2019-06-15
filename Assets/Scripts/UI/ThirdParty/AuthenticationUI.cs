using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AuthenticationUI : MonoBehaviour {

    [Header("Authentication Screen")]
    [SerializeField]
    private GameObject m_RootPanels;

    [Header("Guest Login")]
    [SerializeField]
    private Button m_GuestLoginButton;

    [Header("Facebook Login")]
    [SerializeField]
    private Button m_FacebookLoginButton;

    [Header("Google Play Login")]
    [SerializeField]
    private Button m_GooglePlayLoginButton;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        UnityEngine.Events.UnityAction GuestLoginAction = delegate { Backend.Instance.GuestLogin(LoginGuest, pDisplayName: "Guest_" + UnityEngine.Random.Range(0, 100000)); };
        m_GuestLoginButton.onClick.AddListener(GuestLoginAction);
    }

    void LoginGuest(bool pSuccess, string pUsername)
    {
        if (pSuccess)
        {
            Debug.Log("Welcome: " + pUsername);

            PipelineManager.Instance.RestartFlow();
        }
        else
        {
            Debug.LogError("Error to login as a guest player");
        }
    }
}
