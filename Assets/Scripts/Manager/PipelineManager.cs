using GameSparks.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PipelineManager : MonoBehaviour {

    public enum STEPS {
        VERIFY_CONNECTION,
        VERIFY_AUTHENTICATION,
        VERIFY_VERSION,
        VERIFY_UPDATED
    }

    private static PipelineManager m_Instance;

    private Dictionary<STEPS, bool> m_Stages = new Dictionary<STEPS, bool>();

    private void Awake()
    {
        Instance = this;
    }

    void Start () {
        InitSteps();
        StartCoroutine(CheckPipeline());
    }

    public void RestartFlow()
    {
        StartCoroutine(RestartPipeline());
    }

    void InitSteps()
    {
        m_Stages.Add(STEPS.VERIFY_CONNECTION, false);

        m_Stages.Add(STEPS.VERIFY_VERSION, false);

        m_Stages.Add(STEPS.VERIFY_AUTHENTICATION, false);

        m_Stages.Add(STEPS.VERIFY_UPDATED, false);
    }

    IEnumerator CheckPipeline()
    {
        yield return UnLoadAllScene();

        yield return VerifyServerConnection();

        yield return VerifyVersion();

        yield return VerifyAuthentication();

        yield return VerifyUpdates();

        yield return StartGamePipeline();
    }

    IEnumerator RestartPipeline()
    {
        SceneManager.LoadScene("GameMainScene");

        yield return CheckPipeline();
    }

    IEnumerator StartGamePipeline()
    {
        yield return Connection();
    }

    #region Verifies 
    IEnumerator VerifyServerConnection()
    {
        
        while(true)
        // for (int i = 0; i < 10; i++)
        {
            m_Stages[STEPS.VERIFY_CONNECTION] = true;
            break;
            if (GS.Available)
            {
                m_Stages[STEPS.VERIFY_CONNECTION] = true;
                break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator VerifyVersion()
    {
        // to do
        m_Stages[STEPS.VERIFY_VERSION] = true;
        yield return 0;
    }

    IEnumerator VerifyAuthentication()
    {
        m_Stages[STEPS.VERIFY_AUTHENTICATION] = GS.Authenticated;
        
        yield return 0;
    }

    IEnumerator VerifyUpdates()
    {
        // to do 
        m_Stages[STEPS.VERIFY_UPDATED] = true;
        yield return 0;
    }
    #endregion

    IEnumerator Connection()
    {
        if (m_Stages[STEPS.VERIFY_CONNECTION])
        {
            yield return Version();
        }
        else
        {
            Debug.LogError("// TODO OFFLINE MODE");
        }
    }

    IEnumerator Version()
    {
        if (m_Stages[STEPS.VERIFY_VERSION])
        {
            yield return Authentication();
        }
        else
        {
            Debug.LogError("// TODO Load force update scene");
        }
    }

    IEnumerator Authentication()
    {
        if (m_Stages[STEPS.VERIFY_AUTHENTICATION])
        {
            yield return LoadScene("Beta");
            yield return SceneManager.UnloadSceneAsync("ServerSetup");
        }
        else
        {
            yield return LoadScene("AuthenticationScene");
            yield return SceneManager.UnloadSceneAsync("ServerSetup");
        }
    }

    IEnumerator UpdateFiles()
    {
        if (m_Stages[STEPS.VERIFY_UPDATED]) // data assets updated
        {
            Debug.Log("// TO DO UPDATED FILES");
        }
        else //download update scene
        {
            Debug.LogError("// TO DO UPDATES SCENE");
        }
        yield return 0;
    }

    public AsyncOperation LoadScene(string pName)
    {
        if (!SceneManager.GetSceneByName(pName).isLoaded)
            return SceneManager.LoadSceneAsync(pName, LoadSceneMode.Additive);
        else
            return ReLoadScene(pName);
    }

    AsyncOperation ReLoadScene(string pName)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(pName));
        return SceneManager.LoadSceneAsync(pName, LoadSceneMode.Additive);
    }

    IEnumerator UnLoadAllScene()
    {
        if(SceneManager.sceneCount > 1)
        {
            yield return SceneManager.LoadSceneAsync("GameMainScene", LoadSceneMode.Single);
        }
        yield return SceneManager.LoadSceneAsync("ServerSetup", LoadSceneMode.Additive);

        //for (int i = scenemanager.scenecount - 1; i > 0; i--)
        //{
        //    yield return scenemanager.unloadsceneasync(i);
        //}
    }

    public static PipelineManager Instance
    {
        get
        {
            return m_Instance;
        }

        set
        {
            m_Instance = value;
        }
    }

}
