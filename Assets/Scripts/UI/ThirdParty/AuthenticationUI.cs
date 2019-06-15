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
        m_GuestLoginButton.onClick.AddListener( delegate { Backend.Instance.GuestLogin(LoginGuest); });
    }

    void LoginGuest(bool pSuccess, string pUsername)
    {
        if (pSuccess)
        {
            Debug.Log("Welcome: " + pUsername);
        }
        else
        {
            Debug.LogError("Error to login as a guest player");
        }
    }
}
