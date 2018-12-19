using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager Instance;
    public enum SCREEN{
        GAMEPLAY = 1,
        MENU = 2,
        PAUSE = 3,
        GAMEOVER = 4
    }

    [System.Serializable]
    public class UIScreen
    {
        public GameObject m_pScreenObject;
        public SCREEN m_pScreen;
    }

    public void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private List<UIScreen> m_pScreens = new List<UIScreen>();

    public void OpenScreen(SCREEN pScreen)
    {
        GetScreenObject(pScreen).SetActive(true);
    }

    public void CloseScreen(SCREEN pScreen)
    {

        GetScreenObject(pScreen).SetActive(false);
    }

    public GameObject GetScreenObject(SCREEN pScreen)
    {
        UIScreen UI = m_pScreens.Find(screen => screen.m_pScreen == pScreen);
        return UI.m_pScreenObject;
    }

}
