using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenaryItemUIBeta : MonoBehaviour {

    /*
    [SerializeField]
    private Image m_pScenaryImage;
     */
    [SerializeField]
    private Button m_pSelectButton;

    public enum U_Button
    {
        CONFIGURATIONS = 1,
        LAB = 2,
        STORE = 3,
        TOVORTEX = 4,
        ROADMAP = 5,
        CHANGEVORTEXMAP = 6,
        EXITBUTTON = 7
    }
    [SerializeField]
    private U_Button m_pTypeButton;


    private void Start()
    {
       UpdateSelectButton();
    }

    public void SelectedItem()
    {
        /*
        // get component game play UI to set background UI
        GamePlayUI tGamePlayUI = UIManager.Instance.GetScreenObject(UIManager.SCREEN.GAMEPLAY).GetComponent<GamePlayUI>();
        tGamePlayUI.SetBackground(m_pScenaryImage.sprite);

        GameManager.Instance.ResetBoard();
        */

        //Open UI game play and close menu UI
        //UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREEN.GAMEPLAY);

        //UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREEN.MENU);

        switch (m_pTypeButton)
        {
            case U_Button.LAB:
                // open lab screen

                ScreenManager.Instance.OpenScreen(ScreenManager.SCREEN.LAB);

                break;
            case U_Button.STORE:
                // open store screen

                ScreenManager.Instance.OpenScreen(ScreenManager.SCREEN.STORE);

                break;
            case U_Button.TOVORTEX:
                // open vortex map 

                ScreenManager.Instance.OpenScreen(ScreenManager.SCREEN.ROADMAP_VORTEX);
                ScreenManager.Instance.CloseScreen(ScreenManager.SCREEN.ROADMAP);

                break;
            case U_Button.ROADMAP:
                // open road map 

                ScreenManager.Instance.OpenScreen(ScreenManager.SCREEN.ROADMAP);
                ScreenManager.Instance.CloseScreen(ScreenManager.SCREEN.ROADMAP_VORTEX);

                break;
            case U_Button.CONFIGURATIONS:
                // open config screen
                ScreenManager.Instance.OpenScreen(ScreenManager.SCREEN.CONFIG);

                break;

            case U_Button.EXITBUTTON:
                // change theme map


                break;
        }
    }


    public void UpdateSelectButton()
    {
       m_pSelectButton.onClick = new Button.ButtonClickedEvent();
       m_pSelectButton.onClick.AddListener(SelectedItem);
      
    }

    /*
    public Image PScenaryImage
    {
        get
        {
            return m_pScenaryImage;
        }

        set
        {
            m_pScenaryImage = value;
        }
    }
    */
}

