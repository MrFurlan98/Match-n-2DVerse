using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadMapUI : MonoBehaviour
{
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

    private void Start()
    {
        m_ConfigButton.onClick = new Button.ButtonClickedEvent();
        m_ConfigButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.CONFIGURATIONS);
            });


        m_LabButton.onClick = new Button.ButtonClickedEvent();
        m_LabButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.LAB);
            });

        m_StoreButton.onClick = new Button.ButtonClickedEvent();
        m_StoreButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.STORE);
            });

        m_VortexButton.onClick = new Button.ButtonClickedEvent();
        m_VortexButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.TOVORTEX);
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.BUTTONS.ROADMAP);
            });

        m_PerfilButton.onClick = new Button.ButtonClickedEvent();
        m_PerfilButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.PERFIL);
            });

        m_PlayButton.onClick = new Button.ButtonClickedEvent();
        m_PlayButton.onClick.AddListener(
            delegate {
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.BUTTONS.MENUROADB);
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.BUTTONS.PLAYB);
            });

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
