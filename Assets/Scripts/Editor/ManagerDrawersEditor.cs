using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ManagerDrawersEditor : EditorWindow {
    static string PATH_ICONS = "Data/Icons/";

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
        Object[] tIconsObjects = AssetDatabase.LoadAllAssetsAtPath(PATH_ICONS);

        if (tIconsObjects == null)
            return;

        for (int i = 0; i < tIconsObjects.Length; i++)
        {
            Icon tIcon = tIconsObjects[i] as Icon;
            DrawIcon(tIcon);
        }
    }


    void DrawIcon(Icon pIcon)
    {
        EditorGUILayout.BeginHorizontal();

        GUILayout.Label(pIcon.IconSprite.texture, GUILayout.Width(30), GUILayout.Height(30));

        if (GUILayout.Button("Select"))
        {
            IconEditor.Init();
        }


        EditorGUILayout.EndHorizontal();
    }
}
