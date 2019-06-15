using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.SceneManagement;
public class AuthenticationRunner : Action{

    public override void OnStart()
    {
        StartCoroutine(LoadAuthenticationScene());
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        return Backend.Instance.WasAuthenticated() ? TaskStatus.Success : TaskStatus.Running;
    }

    public override void OnEnd()
    {
        StartCoroutine(CloseAuthenticationScene());
        base.OnEnd();
    }

    IEnumerator LoadAuthenticationScene()
    {
        yield return SceneManager.LoadSceneAsync("AuthenticationScene", LoadSceneMode.Additive);
    }
    IEnumerator CloseAuthenticationScene()
    {
        yield return SceneManager.UnloadSceneAsync("AuthenticationScene");
    }
}
