using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabUI : MonoBehaviour {

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
        
    //roadmap avatar
    [SerializeField]
    private Image m_HeadRoadAvatar;
    [SerializeField]
    private Image m_ArmRoadAvatar;
    [SerializeField]
    private Image m_LegRoadAvatar;
    [SerializeField]
    private Image m_BodyRoadAvatar;

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

    //gameplay avatar
    [SerializeField]
    private Image m_HeadGamePlayAvatar;
    [SerializeField]
    private Image m_ArmGamePlayAvatar;
    [SerializeField]
    private Image m_LegGamePlayAvatar;
    [SerializeField]
    private Image m_BodyGamePlayAvatar;    
    
    //perfil avatar
    [SerializeField]
    private Image m_HeadPerfilAvatar;
    [SerializeField]
    private Image m_ArmPerfilAvatar;
    [SerializeField]
    private Image m_LegPerfilAvatar;
    [SerializeField]
    private Image m_BodyPerfilAvatar;

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

    public Text qntMember;

    private int atualChild=0;
    

    public Image bodyPart;

    private int atualPart = 0;
    private int numberPart = 1;
    private int openOrClose = 0;
    private int lengthArray;

    private void Start()
    {
        partsManager = PartsManager.instance;
        lengthArray = partsManager.headParts.Count;

        bodyPart.sprite = partsManager.headParts[atualPart].part.memberSprite;
        qntMember.text = partsManager.headParts[atualPart].qnt + "x";

        m_ExitButton.onClick = new Button.ButtonClickedEvent();
        m_ExitButton.onClick.AddListener(
            delegate
            {
                openOrClose = 0;
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.BUTTONS.EDITPARTSGO);
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.BUTTONS.LAB);
            });

        m_HeadButton.onClick = new Button.ButtonClickedEvent();
        m_HeadButton.onClick.AddListener(
            delegate
            {
                atualPart = 0;
                bodyPart.sprite = partsManager.headParts[atualPart].part.memberSprite;
                qntMember.text = partsManager.headParts[atualPart].qnt + "x";
                lengthArray = partsManager.headParts.Count;
                numberPart = 1;
            });

        m_BodyButton.onClick = new Button.ButtonClickedEvent();
        m_BodyButton.onClick.AddListener(
            delegate
            {
                atualPart = 0;
                bodyPart.sprite = partsManager.bodyParts[atualPart].part.memberSprite;
                qntMember.text = partsManager.bodyParts[atualPart].qnt + "x";
                lengthArray = partsManager.bodyParts.Count;
                numberPart = 2;
            });

        m_ArmButton.onClick = new Button.ButtonClickedEvent();
        m_ArmButton.onClick.AddListener(
            delegate
            {
                atualPart = 0;
                bodyPart.sprite = partsManager.armParts[atualPart].part.memberSprite;
                qntMember.text = partsManager.armParts[atualPart].qnt + "x";
                lengthArray = partsManager.armParts.Count;
                numberPart = 3;
            });

        m_LegsButton.onClick = new Button.ButtonClickedEvent();
        m_LegsButton.onClick.AddListener(
            delegate
            {
                atualPart = 0;
                bodyPart.sprite = partsManager.legParts[atualPart].part.memberSprite;
                qntMember.text = partsManager.legParts[atualPart].qnt + "x";
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
                    bodyPart.sprite = partsManager.headParts[atualPart].part.memberSprite;
                    qntMember.text = partsManager.headParts[atualPart].qnt + "x";
                }

                if (numberPart == 2)
                {
                    bodyPart.sprite = partsManager.bodyParts[atualPart].part.memberSprite; 
                    qntMember.text = partsManager.bodyParts[atualPart].qnt + "x";                
                }

                if (numberPart == 3)
                {
                    bodyPart.sprite = partsManager.armParts[atualPart].part.memberSprite;
                    qntMember.text = partsManager.armParts[atualPart].qnt + "x";
                
                }
                if (numberPart == 4)
                {
                    bodyPart.sprite = partsManager.legParts[atualPart].part.memberSprite;
                    qntMember.text = partsManager.legParts[atualPart].qnt + "x";
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
                    bodyPart.sprite = partsManager.headParts[atualPart].part.memberSprite;
                    qntMember.text = partsManager.headParts[atualPart].qnt + "x";
                }

                if (numberPart == 2)
                {
                    bodyPart.sprite = partsManager.bodyParts[atualPart].part.memberSprite;
                    qntMember.text = partsManager.bodyParts[atualPart].qnt + "x";
                }

                if (numberPart == 3)
                {
                     bodyPart.sprite = partsManager.armParts[atualPart].part.memberSprite;
                     qntMember.text = partsManager.armParts[atualPart].qnt + "x";
                }

                if (numberPart == 4)
                {
                    bodyPart.sprite = partsManager.legParts[atualPart].part.memberSprite;
                    qntMember.text = partsManager.legParts[atualPart].qnt + "x";
                }
            });

        m_ChosePartsScreenButton.onClick = new Button.ButtonClickedEvent();
        m_ChosePartsScreenButton.onClick.AddListener(
            delegate
            {
                if (openOrClose == 1)
                {
                    openOrClose = 0;
                    UIManagerBeta.Instance.CloseScreen(UIManagerBeta.BUTTONS.EDITPARTSGO);
                }else
                {
                    openOrClose = 1;
                    UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.EDITPARTSGO);
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

    #region func gambi
    private void SetAvatars()
    {
        //lab avatar
        m_HeadLabAvatar.sprite = m_HeadButton.GetComponent<Image>().sprite;
        m_LegLabAvatar.sprite = m_LegsButton.GetComponent<Image>().sprite;
        m_ArmLabAvatar.sprite = m_ArmButton.GetComponent<Image>().sprite;
        m_BodyLabAvatar.sprite = m_BodyButton.GetComponent<Image>().sprite;

        //Roadmap avatar
        m_HeadRoadAvatar.sprite = m_HeadButton.GetComponent<Image>().sprite;
        m_LegRoadAvatar.sprite = m_LegsButton.GetComponent<Image>().sprite;
        m_ArmRoadAvatar.sprite = m_ArmButton.GetComponent<Image>().sprite;
        m_BodyRoadAvatar.sprite = m_BodyButton.GetComponent<Image>().sprite;

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
        m_HeadGamePlayAvatar.sprite = m_HeadButton.GetComponent<Image>().sprite;
        m_LegGamePlayAvatar.sprite = m_LegsButton.GetComponent<Image>().sprite;
        m_ArmGamePlayAvatar.sprite = m_ArmButton.GetComponent<Image>().sprite;
        m_BodyGamePlayAvatar.sprite = m_BodyButton.GetComponent<Image>().sprite;

        //Perfil avatar
        m_HeadPerfilAvatar.sprite = m_HeadButton.GetComponent<Image>().sprite;
        m_LegPerfilAvatar.sprite = m_LegsButton.GetComponent<Image>().sprite;
        m_ArmPerfilAvatar.sprite = m_ArmButton.GetComponent<Image>().sprite;
        m_BodyPerfilAvatar.sprite = m_BodyButton.GetComponent<Image>().sprite;

    }


    #endregion


}
