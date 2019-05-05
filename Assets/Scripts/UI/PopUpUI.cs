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
    [SerializeField]
    private MobPart[] allParts;
    PartsManager partsManager;
    [SerializeField]
    private Button m_rollNumberIndexPart;
    [SerializeField]
    private Image m_imgPart;

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

        partsManager = PartsManager.instance;

        m_Content.SetActive(true);
        m_Title.text = pTitle;
        m_Message.text = pMessage;
        m_Button.onClick.RemoveAllListeners();
        m_Button.onClick.AddListener(delegate() {
            OnClickAction.Invoke();
            m_Content.SetActive(false);
        });

        m_Button.onClick.AddListener(delegate () {
            RollRandomNumber(); 
        });
    }

    private void RollRandomNumber()
    {
        int number;
        number = 2; // Random.Range(0, 3);
        SendToList(number);
        print("x");
    }

    public void SendToList(int indexArray)
    {
        if (allParts[indexArray].numberPart == 0)
        {
            partsManager.AddHead(allParts[indexArray]);
            print("a");
        }

        if (allParts[indexArray].numberPart == 1)
        {
            partsManager.AddLeg(allParts[indexArray]);
            print("b");
        }

        if (allParts[indexArray].numberPart == 2)
        {
            partsManager.AddArm(allParts[indexArray]);
            print("c");
        }

        if (allParts[indexArray].numberPart == 3)
        {
            partsManager.AddBody(allParts[indexArray]);
            print("d");
        }
    }
}
