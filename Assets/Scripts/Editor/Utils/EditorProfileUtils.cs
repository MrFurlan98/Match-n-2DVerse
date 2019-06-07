using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class EditorProfileUtils : EditorWindow {
    [MenuItem("Editor Utilities/ Clear Player Prefs")]
    static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Editor Utilities/ Load All Scenes")]
    static void LoadAllScenes()
    {
        if (EditorSceneManager.GetActiveScene().name == "GameMainScene")
        {
            EditorSceneManager.OpenScene("Assets/Scenes/VerifyStageScenes/ServerSetup.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/Scenes/VerifyStageScenes/AuthenticationScene.unity", OpenSceneMode.Additive);
            EditorSceneManager.OpenScene("Assets/Scenes/Beta.unity", OpenSceneMode.Additive);
        }
    }


}
