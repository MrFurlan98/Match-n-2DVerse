
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
        GAMEPLAY = 9
    }

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

    public void OpenScreen(SCREENS pScreen)
    {
        GetScreenObject(pScreen).SetActive(true);
    }

    public void CloseScreen(SCREENS pScreen)
    {

        GetScreenObject(pScreen).SetActive(false);
    }

    public GameObject GetScreenObject(SCREENS pScreen)
    {
        UIScreen UI = m_pScreens.Find(screen => screen.m_pScreen == pScreen);
        return UI.m_pScreenObject;
    }

}
