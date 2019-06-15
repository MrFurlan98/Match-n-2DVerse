using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.SceneManagement;
public class StartGameRunner :  Action {

    public SharedBool isOnline;

    private bool waitLoading;
    public override void OnStart()
    {
        StartLoading();

        if (isOnline.Value)
        {
            StartCoroutine(LoadingOnlineGameScene());
        }
        else
        {
            StartCoroutine(LoadingOfflineGameScene());
        }
    }

    public override TaskStatus OnUpdate()
    {
        return waitLoading ? TaskStatus.Running : TaskStatus.Success;
    }

    IEnumerator LoadingOnlineGameScene()
    {
        yield return SceneManager.LoadSceneAsync("Beta", LoadSceneMode.Additive);
        //TO - DO Set proprieties of online gamescene


        FinishLoading();
    }

    IEnumerator LoadingOfflineGameScene()
    {
        yield return SceneManager.LoadSceneAsync("Beta", LoadSceneMode.Additive);
        //TO - DO Set proprieties of online gamescene


        FinishLoading();
    }

    private void StartLoading()
    {
        waitLoading = true;

    }

    private void FinishLoading()
    {
        waitLoading = false;
    }
}
