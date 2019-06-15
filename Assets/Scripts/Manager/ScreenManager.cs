
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour {
    private static ScreenManager instance;


    public enum SCREEN{
        GAMEPLAY,
        STORE,
        LAB,
        CONFIG,
        ROADMAP,
        PERFIL,
        ROADMAP_VORTEX

    }

    [Header("Current Screens Opened")]
    [SerializeField]
    private List<SCREEN> m_OpenedScreens = new List<SCREEN>();


    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        OpenScreen(SCREEN.ROADMAP);
    }

    public void OpenScreen(SCREEN pScreen)
    {
        if (!m_OpenedScreens.Contains(pScreen))
        {
            StartCoroutine(LoadScreen(pScreen));
        }
    }

    public void CloseScreen(SCREEN pScreen)
    {
        if (m_OpenedScreens.Contains(pScreen))
        {
            StartCoroutine(UnloadScreen(pScreen));
        }
    }

    IEnumerator LoadScreen(SCREEN pScreen)
    {
        m_OpenedScreens.Add(pScreen);
        yield return SceneManager.LoadSceneAsync(GetScreenName(pScreen), LoadSceneMode.Additive);
    }

    IEnumerator UnloadScreen(SCREEN pScreen)
    {
        yield return SceneManager.UnloadSceneAsync(GetScreenName(pScreen));
        m_OpenedScreens.Remove(pScreen);
    }

    private string GetScreenName(SCREEN pScreen)
    {
        string tScreenName = "";
        switch (pScreen)
        {
            case SCREEN.GAMEPLAY:
                tScreenName = "GamePlaySubScene";
                break;
            case SCREEN.STORE:
                tScreenName = "StoreUISubScene";
                break;
            case SCREEN.LAB:
                tScreenName = "LabUISubScene";
                break;
            case SCREEN.CONFIG:
                tScreenName = "ConfigUISubScene";
                break;
            case SCREEN.ROADMAP:
                tScreenName = "RoadMapUISubScene";
                break;
            case SCREEN.ROADMAP_VORTEX:
                tScreenName = "RoadMapVortexUISubScene";
                break;
            case SCREEN.PERFIL:
                tScreenName = "PerfilUISubScene";
                break;
        }
        return tScreenName;
    }
    public static ScreenManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }
}
