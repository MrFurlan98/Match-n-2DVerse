
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerBeta : MonoBehaviour
{
    public static UIManagerBeta Instance;
    public enum BUTTONS
    {
        CONFIGURATIONS = 1,
        LAB = 2,
        STORE = 3,
        TOVORTEX = 4,
        ROADMAP = 5,
        CHANGEVORTEXMAP = 6,
        PERFIL = 7,
        PLAYB = 8,
        MENUROADB=9,
        GAMEPLAY = 10,
        EDITPARTSGO=11
    }

    [System.Serializable]
    public class UIScreen
    {
        public GameObject m_pScreenObject;
        public BUTTONS m_pScreen;
    }

    public void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private List<UIScreen> m_pScreens = new List<UIScreen>();

    public void OpenScreen(BUTTONS pScreen)
    {
        GetScreenObject(pScreen).SetActive(true);
    }

    public void CloseScreen(BUTTONS pScreen)
    {

        GetScreenObject(pScreen).SetActive(false);
    }

    public GameObject GetScreenObject(BUTTONS pScreen)
    {
        UIScreen UI = m_pScreens.Find(screen => screen.m_pScreen == pScreen);
        return UI.m_pScreenObject;
    }

}
