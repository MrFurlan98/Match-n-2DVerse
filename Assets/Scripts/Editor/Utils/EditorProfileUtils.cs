using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class EditorProfileUtils : EditorWindow {
    [MenuItem("Editor Utilities/ Clear Player Prefs")]
    static void Init()
    {
        PlayerPrefs.DeleteAll();
    }

}
