using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabUI : MonoBehaviour {

    #region Instances
    RoadMapUI instanceRoad;
    #endregion
    #region Gambi
    //lab avatar
    [SerializeField]
    private Image m_HeadLabAvatar;
    [SerializeField]
    private Image m_ArmLabAvatar;
    [SerializeField]
    private Image m_LegLabAvatar;
    [SerializeField]
    private Image m_BodyLabAvatar;
 
    //vortex1 avatar
    [SerializeField]
    private Image m_HeadVortex1Avatar;
    [SerializeField]
    private Image m_ArmVortex1Avatar;
    [SerializeField]
    private Image m_LegVortex1Avatar;
    [SerializeField]
    private Image m_BodyVortex1Avatar;

    //votex2 avatar
    [SerializeField]
    private Image m_HeadVortex2Avatar;
    [SerializeField]
    private Image m_ArmVortex2Avatar;
    [SerializeField]
    private Image m_LegVortex2Avatar;
    [SerializeField]
    private Image m_BodyVortex2Avatar;

    #endregion 

    PartsManager partsManager;

    [SerializeField]
    private Button m_ExitButton;

    [SerializeField]
    private Button m_HeadButton;

    [SerializeField]
    private Button m_BodyButton;

    [SerializeField]
    private Button m_ArmButton;

    [SerializeField]
    private Button m_LegsButton;

    [SerializeField]
    private Button m_RButton;

    [SerializeField]
    private Button m_LButton;

    [SerializeField]
    private Button m_ConfirmPartButton;

    [SerializeField]
    private Button m_ConfirmAvatarButton;

    [SerializeField]
    private Button m_ChosePartsScreenButton;

    private int atualChild=0;

    [SerializeField]
    private GameObject editMemberScreen;
    

    public Image bodyPart;

    private int atualPart = 0;
    private int numberPart = 1;
    private int openOrClose = 0;
    private int lengthArray;

    private void Start()
    {
        instanceRoad = RoadMapUI.instance;
        partsManager = PartsManager.instance;
        lengthArray = partsManager.headParts.Count;

        bodyPart.sprite = partsManager.headParts[atualPart].memberSprite;
        //qntMember.text = partsManager.headParts[atualPart].qnt + "x";

        m_ExitButton.onClick = new Button.ButtonClickedEvent();
        m_ExitButton.onClick.AddListener(
            delegate
            {
                openOrClose = 0;
                editMemberScreen.SetActive(false);
                //UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.EDITPARTSGO);
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.LAB);
            });

        m_HeadButton.onClick = new Button.ButtonClickedEvent();
        m_HeadButton.onClick.AddListener(
            delegate
            {
                atualPart = 0;
                bodyPart.sprite = partsManager.headParts[atualPart].memberSprite;
                //qntMember.text = partsManager.headParts[atualPart].qnt + "x";
                lengthArray = partsManager.headParts.Count;
                numberPart = 1;
            });

        m_BodyButton.onClick = new Button.ButtonClickedEvent();
        m_BodyButton.onClick.AddListener(
            delegate
            {
                atualPart = 0;
                bodyPart.sprite = partsManager.bodyParts[atualPart].memberSprite;
                //qntMember.text = partsManager.bodyParts[atualPart].qnt + "x";
                lengthArray = partsManager.bodyParts.Count;
                numberPart = 2;
            });

        m_ArmButton.onClick = new Button.ButtonClickedEvent();
        m_ArmButton.onClick.AddListener(
            delegate
            {
                atualPart = 0;
                bodyPart.sprite = partsManager.armParts[atualPart].memberSprite;
                //qntMember.text = partsManager.armParts[atualPart].qnt + "x";
                lengthArray = partsManager.armParts.Count;
                numberPart = 3;
            });

        m_LegsButton.onClick = new Button.ButtonClickedEvent();
        m_LegsButton.onClick.AddListener(
            delegate
            {
                atualPart = 0;
                bodyPart.sprite = partsManager.legParts[atualPart].memberSprite;
                //qntMember.text = partsManager.legParts[atualPart].qnt + "x";
                lengthArray = partsManager.legParts.Count;
                numberPart = 4;
            });

        m_ConfirmPartButton.onClick = new Button.ButtonClickedEvent();
        m_ConfirmPartButton.onClick.AddListener(
            delegate
            {
                if (numberPart == 1) m_HeadButton.GetComponent<Image>().sprite = bodyPart.sprite;
                if (numberPart == 2) m_BodyButton.GetComponent<Image>().sprite = bodyPart.sprite;
                if (numberPart == 3) m_ArmButton.GetComponent<Image>().sprite = bodyPart.sprite;
                if (numberPart == 4) m_LegsButton.GetComponent<Image>().sprite = bodyPart.sprite;
            });

        m_RButton.onClick = new Button.ButtonClickedEvent();
        m_RButton.onClick.AddListener(
            delegate
            {
                atualPart++;
                if (atualPart == lengthArray) atualPart -= lengthArray;

                if (numberPart == 1)
                {
                    bodyPart.sprite = partsManager.headParts[atualPart].memberSprite;
                    //qntMember.text = partsManager.headParts[atualPart].qnt + "x";
                }

                if (numberPart == 2)
                {
                    bodyPart.sprite = partsManager.bodyParts[atualPart].memberSprite; 
                    //qntMember.text = partsManager.bodyParts[atualPart].qnt + "x";                
                }

                if (numberPart == 3)
                {
                    bodyPart.sprite = partsManager.armParts[atualPart].memberSprite;
                    //qntMember.text = partsManager.armParts[atualPart].qnt + "x";
                
                }
                if (numberPart == 4)
                {
                    bodyPart.sprite = partsManager.legParts[atualPart].memberSprite;
                    //qntMember.text = partsManager.legParts[atualPart].qnt + "x";
                }
            });


        m_LButton.onClick = new Button.ButtonClickedEvent();
        m_LButton.onClick.AddListener(
            delegate
            {
                atualPart--;
                if (atualPart == lengthArray) atualPart += lengthArray;

                if (numberPart == 1)
                {
                    bodyPart.sprite = partsManager.headParts[atualPart].memberSprite;
                    //qntMember.text = partsManager.headParts[atualPart].qnt + "x";
                }

                if (numberPart == 2)
                {
                    bodyPart.sprite = partsManager.bodyParts[atualPart].memberSprite;
                    //qntMember.text = partsManager.bodyParts[atualPart].qnt + "x";
                }

                if (numberPart == 3)
                {
                     bodyPart.sprite = partsManager.armParts[atualPart].memberSprite;
                     //qntMember.text = partsManager.armParts[atualPart].qnt + "x";
                }

                if (numberPart == 4)
                {
                    bodyPart.sprite = partsManager.legParts[atualPart].memberSprite;
                    //qntMember.text = partsManager.legParts[atualPart].qnt + "x";
                }
            });

        m_ChosePartsScreenButton.onClick = new Button.ButtonClickedEvent();
        m_ChosePartsScreenButton.onClick.AddListener(
            delegate
            {
                if (openOrClose == 1)
                {
                    openOrClose = 0;
                    editMemberScreen.SetActive(false);
                    //UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.EDITPARTSGO);
                }else
                {
                    openOrClose = 1;
                    editMemberScreen.SetActive(true);
                    //UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.EDITPARTSGO);
                    m_HeadButton.GetComponent<Image>().sprite = m_HeadLabAvatar.sprite;
                    m_LegsButton.GetComponent<Image>().sprite = m_LegLabAvatar.sprite;
                    m_ArmButton.GetComponent<Image>().sprite = m_ArmLabAvatar.sprite;
                    m_BodyButton.GetComponent<Image>().sprite = m_BodyLabAvatar.sprite;
                }
                                             
            });

        m_ConfirmAvatarButton.onClick = new Button.ButtonClickedEvent();
        m_ConfirmAvatarButton.onClick.AddListener(
            delegate
            {
                SetAvatars();
            });
    }

    private void SetAvatars()
    {
        #region lab avatar
        m_HeadLabAvatar.sprite = m_HeadButton.GetComponent<Image>().sprite;
        m_LegLabAvatar.sprite = m_LegsButton.GetComponent<Image>().sprite;
        m_ArmLabAvatar.sprite = m_ArmButton.GetComponent<Image>().sprite;
        m_BodyLabAvatar.sprite = m_BodyButton.GetComponent<Image>().sprite;
        #endregion
        instanceRoad.setRoadMapMembers(m_HeadButton.GetComponent<Image>(), m_LegsButton.GetComponent<Image>(), m_ArmButton.GetComponent<Image>(), m_BodyButton.GetComponent<Image>());
        #region Gambi
        //Vortex1 avatar
        m_HeadVortex1Avatar.sprite = m_HeadButton.GetComponent<Image>().sprite;
        m_LegVortex1Avatar.sprite = m_LegsButton.GetComponent<Image>().sprite;
        m_ArmVortex1Avatar.sprite = m_ArmButton.GetComponent<Image>().sprite;
        m_BodyVortex1Avatar.sprite = m_BodyButton.GetComponent<Image>().sprite;

        //Votex2 avatar
        m_HeadVortex2Avatar.sprite = m_HeadButton.GetComponent<Image>().sprite;
        m_LegVortex2Avatar.sprite = m_LegsButton.GetComponent<Image>().sprite;
        m_ArmVortex2Avatar.sprite = m_ArmButton.GetComponent<Image>().sprite;
        m_BodyVortex2Avatar.sprite = m_BodyButton.GetComponent<Image>().sprite;

        //Gameplay avatar
        #endregion
    }




}
