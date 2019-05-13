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
    private GameObject m_ButtonReceiveItens;
    [SerializeField]
    private Button m_rollNumberIndexPart;

    [SerializeField]
    private Image m_imgPart;

    //[SerializeField]
    //private MobPart[] allParts;

    [SerializeField]
    private MobPart[] packOneMembers;

    [SerializeField]
    private MobPart[] packTwoMembers;

    [SerializeField]
    private MobPart[] packThreeMembers;

    [SerializeField]
    private MobPart[] packFourMembers;

    [SerializeField]
    private MobPart[] packFiveMembers;

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

    [System.Serializable]
    public class DropRarity
    {
        public string name;
        public int dropRarity;
        public int numberPack;
    }

    public List <DropRarity> lootItens = new List<DropRarity>();


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
            m_ButtonReceiveItens.SetActive(true);
            m_imgPart.sprite = null;
        });

        m_rollNumberIndexPart.onClick.RemoveAllListeners();
        m_rollNumberIndexPart.onClick.AddListener(delegate () {
            RollRandomNumber();
            m_ButtonReceiveItens.SetActive(false);

        });
    }
    

    private void RollRandomNumber()
    {
        int number;
        number = SortItemNumber();
        SearchPack(number);
    }

    private int SortItemNumber()
    {
        int itemWeight = 0;

        for (int i = 0; i < lootItens.Count; i++)
        {
            itemWeight += lootItens[i].dropRarity;
        }

        int randomValue = UnityEngine.Random.Range(0, itemWeight);

        for (int j = 0; j < lootItens.Count; j++)
        {
            if (randomValue <= lootItens[j].dropRarity)
            {
                return lootItens[j].numberPack;
            }
            randomValue -= lootItens[j].dropRarity;
        }
        return 6;
    }


    public void SearchPack(int numberArray)
    {
        if (numberArray == 6)
        {
            print("azedo");
            return;
        }

        if (numberArray == 0)
        {
            int numberMember = RandonNumberMember();
            SendToList(packOneMembers[numberArray]);
        }

        if (numberArray == 1)
        {
            int numberMember = RandonNumberMember();
            SendToList(packOneMembers[numberArray]);

        }

        if (numberArray == 2)
        {
            int numberMember = RandonNumberMember();
            SendToList(packOneMembers[numberArray]);

        }

        if (numberArray == 3)
        {
            int numberMember = RandonNumberMember();
            SendToList(packOneMembers[numberArray]);

        }

        if (numberArray == 4)
        {
            int numberMember = RandonNumberMember();
            SendToList(packOneMembers[numberArray]);

        }

    }

    private int RandonNumberMember()
    {
        int number = UnityEngine.Random.Range(0, 5);
        return number;
    }


    public void SendToList(MobPart memberToSend)
    {
        m_imgPart.sprite = memberToSend.memberSprite;

        if (memberToSend.numberPart == 0)
        {
            partsManager.AddHead(memberToSend);
        }

        if (memberToSend.numberPart == 1)
        {            
            partsManager.AddLeg(memberToSend);
        }
        
        if (memberToSend.numberPart == 2)
        {            
            partsManager.AddArm(memberToSend);
        }

        if (memberToSend.numberPart == 3)
        {
            partsManager.AddBody(memberToSend);
        }
    }
}