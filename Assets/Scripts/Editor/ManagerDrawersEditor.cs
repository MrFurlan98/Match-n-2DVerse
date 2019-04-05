using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ManagerDrawersEditor : EditorWindow {
    static string PATH_ICONS = "/Data/Icons";

    private int m_CurrentEditor = 0;
    private string[] m_ArrayEditors = { "Icon", "Combo" };

    [MenuItem("Window/ Match N/ Editor")]
    static void Init()
    {
        ManagerDrawersEditor tManagerDrawersEditor = GetWindow<ManagerDrawersEditor>();

        tManagerDrawersEditor.minSize = new Vector2(450, 600);
        tManagerDrawersEditor.maxSize = new Vector2(450, 600);

        tManagerDrawersEditor.Show();
    }

    private void OnGUI()
    {
        m_CurrentEditor = GUILayout.Toolbar(m_CurrentEditor, m_ArrayEditors);

        switch (m_CurrentEditor)
        {
            case 0: // icon
                DrawerIconList();
                break;

        }
    }

    void DrawerIconList()
    {
        string tPath = Application.dataPath+PATH_ICONS;
        string[] tFiles = System.IO.Directory.GetFiles(tPath, "*.asset", System.IO.SearchOption.AllDirectories);
        Debug.Log(tFiles.Length);
        if(tFiles == null)
            return;
        for (int i = 0; i < tFiles.Length; i++)
        {

            string assetPath = "Assets"+PATH_ICONS + tFiles[i].Replace(tPath, "").Replace('\\', '/');

             Debug.Log(assetPath);
            Icon tIcon = (Icon)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Icon));
           
            DrawIcon(tIcon, tPath);
        }

    }


    void DrawIcon(Icon pIcon, string pPath)
    {
        EditorGUILayout.BeginHorizontal();

        GUILayout.Label(pIcon.IconSprite.texture, GUILayout.Width(80), GUILayout.Height(80));

        GUILayout.Label(pIcon.Tag);

        if (GUILayout.Button("Select", GUILayout.Width(50)))
        {
            IconEditor.Init();
        }

        if (GUILayout.Button("Delete", GUILayout.Width(50)))
        {
            Debug.Log(pPath+"/"+pIcon.name);
            System.IO.File.Delete(pPath+"/"+pIcon.name+".asset");
            System.IO.File.Delete(pPath+"/"+pIcon.name+".asset.meta");
            AssetDatabase.Refresh();
        }


        EditorGUILayout.EndHorizontal();
    }
}
