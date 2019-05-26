using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpStoreMemberItem : MonoBehaviour {

    private static PopUpStoreMemberItem m_Instance;

    [SerializeField]
    private GameObject m_Content;
    [SerializeField]
    private Image m_imgPart;
    [SerializeField]
    private Button m_Button;

    private int numberRarityChest;

    [SerializeField]
    private MobPart[] packCommomMembers;

    [SerializeField]
    private MobPart[] packRareMembers;

    [SerializeField]
    private MobPart[] packLegendaryMembers;

    PartsManager partsManager;
    InventoryManager invtryInstance;

    public static PopUpStoreMemberItem Instance
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

    public void Start()
    {
        partsManager = PartsManager.instance;
        m_Button.onClick = new Button.ButtonClickedEvent();
        m_Button.onClick.AddListener(delegate () {
            m_Content.SetActive(false);
            m_imgPart.sprite = null;
            //UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.STORE);
        });
    }

    public void OpenPopUp(int numberChest, bool canBuy)
    {
        invtryInstance = InventoryManager.instance;
        m_Content.SetActive(true);
        numberRarityChest = numberChest;
        CheckNumberArray(numberChest);
        //UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.STORE);

    }

    public void CheckNumberArray(int numberChestToSend)
    {
        if (numberRarityChest == 0)
        {
            int randomNumber = RandonNumberMember();
            SendToList(packCommomMembers[randomNumber]);
        }

        if (numberRarityChest == 1)
        {
            int randomNumber = RandonNumberMember();
            SendToList(packRareMembers[randomNumber]);
        }

        if (numberRarityChest == 2)
        {
            int randomNumber = RandonNumberMember();
            SendToList(packLegendaryMembers[randomNumber]);
        }
    }

    private int RandonNumberMember()
    {
        int number = UnityEngine.Random.Range(0, 4);
        return number;
    }

    public void SendToList(MobPart memberToSend)
    {
        m_imgPart.sprite = memberToSend.memberSprite;

        if (memberToSend.numberPart == 0)
        {
            for (int i = 0; i < partsManager.headParts.Count; i++)
            {
                if (partsManager.headParts[i].idMember == memberToSend.idMember)
                {
                    invtryInstance.hardCurrency++;
                    return;
                }
            }
            partsManager.headParts.Add(memberToSend);
        }



        if (memberToSend.numberPart == 1)
        {
            for (int i = 0; i < partsManager.legParts.Count; i++)
            {
                if (partsManager.legParts[i].idMember == memberToSend.idMember)
                {
                    invtryInstance.hardCurrency++;
                    return;
                }
            }
            partsManager.legParts.Add(memberToSend);
        }


        if (memberToSend.numberPart == 2)
        {
            for (int i = 0; i < partsManager.armParts.Count; i++)
            {
                if (partsManager.armParts[i].idMember == memberToSend.idMember)
                {
                    invtryInstance.hardCurrency++;
                    return;
                }
            }
            partsManager.armParts.Add(memberToSend);
        }


        if (memberToSend.numberPart == 3)
        {
            for (int i = 0; i < partsManager.bodyParts.Count; i++)
            {
                if (partsManager.bodyParts[i].idMember == memberToSend.idMember)
                {
                    invtryInstance.hardCurrency++;
                    return;
                }
            }
            partsManager.bodyParts.Add(memberToSend);
        }
    }
}
