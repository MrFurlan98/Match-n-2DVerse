
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerBeta : MonoBehaviour
{
    public static UIManagerBeta Instance;
    public enum SCREENS
    {
        CONFIGURATIONS = 1,
        LAB = 2,
        STORE = 3,
        TOVORTEX = 4,
        ROADMAP = 5,
        CHANGEVORTEXMAP = 6,
        PERFIL = 7,
        AUTHETICATION = 8,
        GAMEPLAY = 9,
        POPUP
    }
    /*public enum PANELS
    {
        RESCUE,
        DEACTIVATE_BOMB,
        UNDER_THE_GORGONAS_EYES,
        ONE_OF_TWELVE_WORKS,
        NONE
    }*/

    /*[System.Serializable]
    public class UIPanel
    {
        public GameObject m_pPanelObject;
        public PANELS m_pPanel;
    }*/

    [System.Serializable]
    public class UIScreen
    {
        public GameObject m_pScreenObject;
        public SCREENS m_pScreen;
    }

    public void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private List<UIScreen> m_pScreens = new List<UIScreen>();

    //[SerializeField]
    //private List<UIPanel> m_pPanel = new List<UIPanel>();

    public void OpenScreen(SCREENS pScreen)
    {
        GetScreenObject(pScreen).SetActive(true);
    }

    public void CloseScreen(SCREENS pScreen)
    {

        GetScreenObject(pScreen).SetActive(false);
    }

    /*public void OpenPanel(PANELS pPanel)
    {
        GetPanelObject(pPanel).SetActive(true);
    }

    public void ClosePanel(PANELS pPanel)
    {

        GetPanelObject(pPanel).SetActive(false);
    }*/

    public GameObject GetScreenObject(SCREENS pScreen)
    {
        UIScreen UI = m_pScreens.Find(screen => screen.m_pScreen == pScreen);
        return UI.m_pScreenObject;
    }
    /*public GameObject GetPanelObject(PANELS pPanel)
    {
        UIPanel UI = m_pPanel.Find(panel => panel.m_pPanel == pPanel);
        return UI.m_pPanelObject;
    }*/
}
