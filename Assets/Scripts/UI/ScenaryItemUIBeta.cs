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

                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.LAB);

                break;
            case U_Button.STORE:
                // open store screen

                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.STORE);

                break;
            case U_Button.TOVORTEX:
                // open vortex map 

                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.TOVORTEX);
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.ROADMAP);

                break;
            case U_Button.ROADMAP:
                // open road map 

                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.ROADMAP);
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.TOVORTEX);

                break;
            case U_Button.CONFIGURATIONS:
                // open config screen
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.CONFIGURATIONS);

                break;

            case U_Button.CHANGEVORTEXMAP:
                // change theme map
                UIManagerBeta.Instance.OpenScreen(UIManagerBeta.SCREENS.CHANGEVORTEXMAP);
                UIManagerBeta.Instance.CloseScreen(UIManagerBeta.SCREENS.TOVORTEX);


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

