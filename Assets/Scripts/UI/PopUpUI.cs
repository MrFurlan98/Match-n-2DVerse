using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpUI : MonoBehaviour {

    private static PopUpUI m_Instance;
    [SerializeField]
    private GameObject m_Content;
    [SerializeField]
    private Text m_Title;
    [SerializeField]
    private Text m_Message;
    [SerializeField]
    private Button m_Button;

    public static PopUpUI Instance
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

    private void Awake()
    {
        Instance = this;
    }

    public void OpenPopUp(string pTitle, string pMessage, Action OnClickAction)
    {
        m_Content.SetActive(true);
        m_Title.text = pTitle;
        m_Message.text = pMessage;
        m_Button.onClick.RemoveAllListeners();
        m_Button.onClick.AddListener(delegate() {
            OnClickAction.Invoke();
            m_Content.SetActive(false);
        });
    }
}
