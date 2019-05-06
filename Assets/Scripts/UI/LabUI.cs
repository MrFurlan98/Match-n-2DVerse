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

    private int atualChild=0;

    public Sprite[] headsMembers;
    public Sprite[] legsMembers;
    public Sprite[] armsMembers;
    public Sprite[] bodyMembers;
    public Sprite[] membersSpritesUsing;

    public Image bodyPart;

    private int atualPart = 0;
    private int numberPart = 1;
    private int openOrClose = 0;

    private void Start()
    {
        partsManager = PartsManager.instance;

        membersSpritesUsing = partsManager.CreateArmSpritesArray();

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
                bodyPart.sprite = m_HeadButton.GetComponent<Image>().sprite;
                atualPart = 0;
                membersSpritesUsing = partsManager.CreateHeadSpritesArray();
                numberPart = 1;
            });

        m_BodyButton.onClick = new Button.ButtonClickedEvent();
        m_BodyButton.onClick.AddListener(
            delegate
            {
                bodyPart.sprite = m_BodyButton.GetComponent<Image>().sprite;
                atualPart = 0;
                membersSpritesUsing = partsManager.CreateBodySpritesArray();
                numberPart = 2;
            });

        m_ArmButton.onClick = new Button.ButtonClickedEvent();
        m_ArmButton.onClick.AddListener(
            delegate
            {
                bodyPart.sprite = m_ArmButton.GetComponent<Image>().sprite;
                atualPart = 0;
                membersSpritesUsing = partsManager.CreateArmSpritesArray();
                numberPart = 3;
            });

        m_LegsButton.onClick = new Button.ButtonClickedEvent();
        m_LegsButton.onClick.AddListener(
            delegate
            {
                bodyPart.sprite = m_LegsButton.GetComponent<Image>().sprite;
                atualPart = 0;
                membersSpritesUsing = partsManager.CreateLegSpritesArray();
                numberPart = 4;
            });

        m_ConfirmPartButton.onClick = new Button.ButtonClickedEvent();
        m_ConfirmPartButton.onClick.AddListener(
            delegate
            {
                if (numberPart == 1) m_HeadButton.GetComponent<Image>().sprite = membersSpritesUsing[atualPart];
                if (numberPart == 2) m_BodyButton.GetComponent<Image>().sprite = membersSpritesUsing[atualPart];
                if (numberPart == 3) m_ArmButton.GetComponent<Image>().sprite = membersSpritesUsing[atualPart];
                if (numberPart == 4) m_LegsButton.GetComponent<Image>().sprite = membersSpritesUsing[atualPart];
            });

        m_RButton.onClick = new Button.ButtonClickedEvent();
        m_RButton.onClick.AddListener(
            delegate
            {
                atualPart++;
                if (atualPart == membersSpritesUsing.Length) atualPart -= membersSpritesUsing.Length;
                bodyPart.sprite = membersSpritesUsing[atualPart];

            });


        m_LButton.onClick = new Button.ButtonClickedEvent();
        m_LButton.onClick.AddListener(
            delegate
            {
                atualPart--;
                if (atualPart < 0) atualPart += membersSpritesUsing.Length;
                bodyPart.sprite = membersSpritesUsing[atualPart];
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
