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
    private GameObject m_ContentReceiveItens;
    [SerializeField]
    private Text m_Title;
    [SerializeField]
    private Text m_Message;
    [SerializeField]
    private Button m_Button;
    [SerializeField]
    private MobPart[] allParts;
    [SerializeField]
    private GameObject m_ButtonReceiveItens;
    [SerializeField]
    private Button m_rollNumberIndexPart;
    [SerializeField]
    private Image m_imgPart;

    PartsManager partsManager;

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

    public void OpenPopUp(string pTitle, bool pResultBoard, Action OnClickAction)
    {

        partsManager = PartsManager.instance;

        if (pResultBoard)
        {
            m_ContentReceiveItens.SetActive(true);
            m_Message.text = "Voce Ganhou";
        }
        else
        {
            m_ContentReceiveItens.SetActive(false);
            m_Message.text = "Voce perdeu";
        }

        m_Content.SetActive(true);
        m_Title.text = pTitle;
        m_Button.onClick.RemoveAllListeners();
        m_Button.onClick.AddListener(delegate() {
            OnClickAction.Invoke();
            m_Content.SetActive(false);
            m_ContentReceiveItens.SetActive(false);
        });

        m_rollNumberIndexPart.onClick.AddListener(delegate () {
            RollRandomNumber();
            m_ButtonReceiveItens.SetActive(false);

        });
    }


    private void RollRandomNumber()
    {
        int number;
        number = SortItemNumber();
        SendToList(number);
    }

    private static int SortItemNumber()
    {
        int number = UnityEngine.Random.Range(0, 19);
        return number;
    }

    public void SendToList(int indexArray)
    {
        m_imgPart.sprite = allParts[indexArray].memberSprite;

        if (allParts[indexArray].numberPart == 0)
        {
            partsManager.AddHead(allParts[indexArray]);
        }

        if (allParts[indexArray].numberPart == 1)
        {
            partsManager.AddLeg(allParts[indexArray]);
        }

        if (allParts[indexArray].numberPart == 2)
        {
            partsManager.AddArm(allParts[indexArray]);
        }

        if (allParts[indexArray].numberPart == 3)
        {
            partsManager.AddBody(allParts[indexArray]);
        }
    }
}
