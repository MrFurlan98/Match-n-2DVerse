using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadMapUI : MonoBehaviour
{
    #region Avatar
    [SerializeField]
    public Image m_HeadRoadAvatar;
    [SerializeField]
    public Image m_ArmRoadAvatar;
    [SerializeField]
    public Image m_LegRoadAvatar;
    [SerializeField]
    public Image m_BodyRoadAvatar;
    #endregion
    #region Instances
    public static RoadMapUI instance;
    PerfilUI instancePerf;
    MinionAvatar instanceMA;
    #endregion

    [SerializeField]
    private Button m_ConfigButton;

    [SerializeField]
    private Button m_LabButton;

    [SerializeField]
    private Button m_StoreButton;

    [SerializeField]
    private Button m_VortexButton;

    [SerializeField]
    private Button m_PerfilButton;

    [SerializeField]
    private Button m_PlayButton;

    [SerializeField]
    private Button m_BoostCross;

    [SerializeField]
    private Button m_BoostExplosion;

    [SerializeField]
    private Button m_BoostSuper;

    [SerializeField]
    private Button m_ClosePopUp;

    [SerializeField]
    private Text m_WelcomeText;

    [SerializeField]
    private GameObject startGameScreen;

    [SerializeField]
    private GameObject mapButtonsScreen;

    [SerializeField]
    private GameObject boostsPopUp;

    [SerializeField]
    private Image boostImage1;

    [SerializeField]
    private Image boostImage2;

    [SerializeField]
    private Image boostImage3;

    [SerializeField]
    private Text textQtdBoostOne;
    [SerializeField]
    private Text textQtdBoostTwo;
    [SerializeField]
    private Text textQtdBoostThree;



    private void Awake()
    {
        instance = this;
        instanceMA = MinionAvatar.instance;
    }

    private void Start()
    {
        m_WelcomeText.text = "Welcome: " + PlayerManager.Instance.PlayerProfileData.UserName;
        m_ConfigButton.onClick = new Button.ButtonClickedEvent();
        m_ConfigButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.CONFIGURATIONS);
            });


        m_LabButton.onClick = new Button.ButtonClickedEvent();
        m_LabButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.LAB);
            });

        m_StoreButton.onClick = new Button.ButtonClickedEvent();
        m_StoreButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.STORE);
                StoreUI.Instance.SetHCText();
            });

        m_VortexButton.onClick = new Button.ButtonClickedEvent();
        m_VortexButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.TOVORTEX);
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.ROADMAP);
            });

        m_PerfilButton.onClick = new Button.ButtonClickedEvent();
        m_PerfilButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.PERFIL);
                instancePerf = PerfilUI.instance;
                instancePerf.setPerfilMembers(instanceMA.m_headMemberUsing, instanceMA.m_legMemberUsing, instanceMA.m_armMemberUsing, instanceMA.m_bodyMemberUsing);
            });

        m_PlayButton.onClick = new Button.ButtonClickedEvent();
        m_PlayButton.onClick.AddListener(
            delegate {
                //UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.MENUROADB);
                mapButtonsScreen.SetActive(true);
                startGameScreen.SetActive(false);
                //UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.PLAYB);
            });
        m_BoostCross.onClick = new Button.ButtonClickedEvent();
        m_BoostCross.onClick.AddListener(
            delegate
            {
                openPopUp(m_BoostCross.GetComponent<Image>(), 0);
            });
        m_BoostExplosion.onClick = new Button.ButtonClickedEvent();
        m_BoostExplosion.onClick.AddListener(
            delegate
            {
                openPopUp(m_BoostExplosion.GetComponent<Image>(), 1);
            });
        m_BoostSuper.onClick = new Button.ButtonClickedEvent();
        m_BoostSuper.onClick.AddListener(
            delegate
            {
                openPopUp(m_BoostSuper.GetComponent<Image>(), 2);
            });

        m_ClosePopUp.onClick = new Button.ButtonClickedEvent();
        m_ClosePopUp.onClick.AddListener(
            delegate
            {
                boostsPopUp.SetActive(false);
            });
    }

    /*public void setRoadMapMembers(Image m_HeadButton, Image m_LegsButton, Image m_ArmButton, Image m_BodyButton)
    {
        m_HeadRoadAvatar.sprite = m_HeadButton.GetComponent<Image>().sprite;
        m_LegRoadAvatar.sprite = m_LegsButton.GetComponent<Image>().sprite;
        m_ArmRoadAvatar.sprite = m_ArmButton.GetComponent<Image>().sprite;
        m_BodyRoadAvatar.sprite = m_BodyButton.GetComponent<Image>().sprite;
    }*/


    /*m_BoostCross.onClick = new Button.ButtonClickedEvent();
        m_BoostCross.onClick.AddListener(
            delegate
            {

                if (InventoryManager.instance.listBoosts[0].qtdItem > 0) { 
                    BoostManager.Instance.StarBoostEffect("7Bomb");
                    InventoryManager.instance.listBoosts[0].qtdItem--;
                    UpdateText();
}
            });
        m_BoostExplosion.onClick = new Button.ButtonClickedEvent();
        m_BoostExplosion.onClick.AddListener(
            delegate
            {
                if (InventoryManager.instance.listBoosts[1].qtdItem > 0) { 
                    BoostManager.Instance.StarBoostEffect("6Bomb");
                    InventoryManager.instance.listBoosts[1].qtdItem--;
                    UpdateText();
                }
            });
        m_BoostSuper.onClick = new Button.ButtonClickedEvent();
        m_BoostSuper.onClick.AddListener(
            delegate
            {
                if (InventoryManager.instance.listBoosts[2].qtdItem > 0) { 
                    BoostManager.Instance.StarBoostEffect("Super");
                    InventoryManager.instance.listBoosts[2].qtdItem--;
                    UpdateText();
                }
            });

    */

    private void openPopUp(Image boostToUse, int numberArrayBoost)
    {
        boostsPopUp.SetActive(true);
        boostImage1.sprite = boostToUse.sprite;
        boostImage2.sprite = boostToUse.sprite;
        boostImage3.sprite = boostToUse.sprite;
        UpdateText(numberArrayBoost);
    }


    public void UpdateText(int numberArrayToUptade)
    {
        textQtdBoostOne.text = InventoryManager.instance.listBoosts[numberArrayToUptade].qtdItem + "x";
        textQtdBoostTwo.text = InventoryManager.instance.listBoosts[numberArrayToUptade + 3].qtdItem + "x";
        textQtdBoostThree.text = InventoryManager.instance.listBoosts[numberArrayToUptade + 6].qtdItem + "x";
    }


    ///*
    //[SerializeField]
    //private Image m_pScenaryImage;
    // */


    //public enum U_Button
    //{
    //    CONFIGURATIONS = 1,
    //    LAB = 2,
    //    STORE = 3,
    //    TOVORTEX = 4
    //}


    //[SerializeField]
    //private Button m_pSelectButton;
    //[SerializeField]
    //private U_Button m_pTypeButton;


    //private void Start()
    //{
    //    //UpdateSelectButton();
    //}

    //public void SelectedItem()
    //{
    //    /*
    //    // get component game play UI to set background UI
    //    GamePlayUI tGamePlayUI = UIManager.Instance.GetScreenObject(UIManager.SCREEN.GAMEPLAY).GetComponent<GamePlayUI>();
    //    tGamePlayUI.SetBackground(m_pScenaryImage.sprite);

    //    GameManager.Instance.ResetBoard();
    //    */

    //    //Open UI game play and close menu UI
    //    //UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREEN.GAMEPLAY);

    //    //UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREEN.MENU);

    //    switch (m_pTypeButton)
    //    {
    //        case U_Button.LAB:
    //            // open lab screen

    //            UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.LAB);

    //            break;
    //        case U_Button.STORE:
    //            // open store screen

    //            UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.STORE);

    //            break;
    //        case U_Button.TOVORTEX:
    //            // open vortex map 

    //            UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.TOVORTEX);
    //            UIManagerBeta.Instance.CloseScreen(UIManagerBeta.BUTTONS.ROADMAP);

    //            break;

    //        case U_Button.CONFIGURATIONS:
    //            // open config screen
    //            UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.CONFIGURATIONS);

    //            break;
    //    }
    //}

    ///*
    //public void UpdateSelectButton()
    //{
    //    m_pSelectButton.onClick = new Button.ButtonClickedEvent();
    //    m_pSelectButton.onClick.AddListener(SelectedItem);

    //}

}
