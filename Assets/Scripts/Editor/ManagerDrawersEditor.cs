using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ManagerDrawersEditor : EditorWindow {
    static string PATH_ICONS = "/Data/Icons";
    static string PATH_SPECIAL_ICONS = "/Data/SpecialIcons";
    static string PATH_COMBOS = "/Data/Combos";
    static string PATH_BOOSTS = "/Data/Boosts";

    private Vector2 m_ActionView;

    private int m_CurrentEditor = 0;
    private string[] m_ArrayEditors = { "Icon","Special Icon", "Combo","Boost" };

    private string tName = "";

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

        tName = EditorGUILayout.TextField(tName);

        switch (m_CurrentEditor)
        {
            case 0: // icon
                DrawAdd<Icon>(tName, PATH_ICONS, ScriptableObject.CreateInstance<Icon>());
                DrawerIconList();
                break;
            case 1: // Special icon
                DrawAdd<SpecialIcon>(tName, PATH_SPECIAL_ICONS, ScriptableObject.CreateInstance<SpecialIcon>());
                DrawerSpecialIconList();
                break;
            case 2: // Combo
                DrawAdd<Combo>("Combo", PATH_COMBOS, ScriptableObject.CreateInstance<Combo>());
                DrawerComboList();
                break;
            case 3: // Boost
                DrawAdd<Boost>("Boost", PATH_BOOSTS, ScriptableObject.CreateInstance<Boost>());
                DrawerBoostList();
                break;
        }
    }

    private void DrawAdd<T>(string tName, string pPATH, T tType) where T : ScriptableObject
    {
        if (GUILayout.Button("New"))
        {
            string assetPath = "Assets" + pPATH;
            string tFullPath = AssetDatabase.GenerateUniqueAssetPath(assetPath + "/" + tName + ".asset");
            AssetDatabase.CreateAsset(tType, tFullPath);
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    void DrawerIconList()
    {
        string tPath = Application.dataPath+PATH_ICONS;
        string[] tFiles = System.IO.Directory.GetFiles(tPath, "*.asset", System.IO.SearchOption.AllDirectories);
        Debug.Log(tFiles.Length);
        if(tFiles == null)
            return;
        m_ActionView = EditorGUILayout.BeginScrollView(m_ActionView);
        for (int i = 0; i < tFiles.Length; i++)
        {

            string assetPath = "Assets"+PATH_ICONS + tFiles[i].Replace(tPath, "").Replace('\\', '/');

             Debug.Log(assetPath);
            Icon tIcon = (Icon)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Icon));
           
            DrawIcon(tIcon, tPath);
        }
        EditorGUILayout.EndScrollView();
    }
    void DrawerSpecialIconList()
    {
        string tPath = Application.dataPath + PATH_SPECIAL_ICONS;
        string[] tFiles = System.IO.Directory.GetFiles(tPath, "*.asset", System.IO.SearchOption.AllDirectories);
        Debug.Log(tFiles.Length);
        if (tFiles == null)
            return;
        m_ActionView = EditorGUILayout.BeginScrollView(m_ActionView);
        for (int i = 0; i < tFiles.Length; i++)
        {

            string assetPath = "Assets" + PATH_SPECIAL_ICONS + tFiles[i].Replace(tPath, "").Replace('\\', '/');

            Debug.Log(assetPath);
            SpecialIcon tIcon = (SpecialIcon)AssetDatabase.LoadAssetAtPath(assetPath, typeof(SpecialIcon));

            DrawIcon(tIcon, tPath);
        }
        EditorGUILayout.EndScrollView();

    }
    void DrawerComboList()
    {
        string tPath = Application.dataPath + PATH_COMBOS;
        string[] tFiles = System.IO.Directory.GetFiles(tPath, "*.asset", System.IO.SearchOption.AllDirectories);
        Debug.Log(tFiles.Length);
        if (tFiles == null)
            return;
        m_ActionView = EditorGUILayout.BeginScrollView(m_ActionView);
        for (int i = 0; i < tFiles.Length; i++)
        {

            string assetPath = "Assets" + PATH_COMBOS + tFiles[i].Replace(tPath, "").Replace('\\', '/');

            Debug.Log(assetPath);
            Combo tCombo = (Combo)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Combo));

            DrawCombo(tCombo, tPath);
        }
        EditorGUILayout.EndScrollView();
    }
    void DrawerBoostList()
    {
        string tPath = Application.dataPath + PATH_BOOSTS;
        string[] tFiles = System.IO.Directory.GetFiles(tPath, "*.asset", System.IO.SearchOption.AllDirectories);
        Debug.Log(tFiles.Length);
        if (tFiles == null)
            return;
        m_ActionView = EditorGUILayout.BeginScrollView(m_ActionView);
        for (int i = 0; i < tFiles.Length; i++)
        {

            string assetPath = "Assets" + PATH_BOOSTS + tFiles[i].Replace(tPath, "").Replace('\\', '/');

            Debug.Log(assetPath);
            Boost tBoost = (Boost)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Boost));

            DrawBoost(tBoost, tPath);
        }
        EditorGUILayout.EndScrollView();
    }
    void DrawIcon(Icon pIcon, string pPath)
    {
        EditorGUILayout.BeginHorizontal();
        if(pIcon.IconSprite != null)
            GUILayout.Label(pIcon.IconSprite.texture, GUILayout.Width(80), GUILayout.Height(80));

        GUILayout.Label(pIcon.Tag);

        if (GUILayout.Button("Select", GUILayout.Width(50)))
        {
            IconEditor.Init(ref pIcon);
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
    void DrawBoost(Boost pBoost, string pPath)
    {
        EditorGUILayout.BeginHorizontal();
        if (pBoost.BoostSprite != null)
            GUILayout.Label(pBoost.BoostSprite.texture, GUILayout.Width(80), GUILayout.Height(80));

        GUILayout.Label(pBoost.Tag);

        if (GUILayout.Button("Select", GUILayout.Width(50)))
        {
            BoostEditor.Init(ref pBoost);
        }

        if (GUILayout.Button("Delete", GUILayout.Width(50)))
        {
            Debug.Log(pPath + "/" + pBoost.name);
            System.IO.File.Delete(pPath + "/" + pBoost.name + ".asset");
            System.IO.File.Delete(pPath + "/" + pBoost.name + ".asset.meta");
            AssetDatabase.Refresh();
        }


        EditorGUILayout.EndHorizontal();
    }
    void DrawCombo(Combo pCombo, string pPath)
    {
        
        EditorGUILayout.BeginHorizontal();
        if(pCombo.Icon1 != null && pCombo.Icon1.IconSprite != null)
            GUILayout.Label(pCombo.Icon1.IconSprite.texture, GUILayout.Width(80), GUILayout.Height(80));
        if (pCombo.Icon2 != null && pCombo.Icon2.IconSprite != null)
            GUILayout.Label(pCombo.Icon2.IconSprite.texture, GUILayout.Width(80), GUILayout.Height(80));

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Select", GUILayout.Width(50)))
        {
            ComboEditor.Init(ref pCombo);
        }

        if (GUILayout.Button("Delete", GUILayout.Width(50)))
        {
            Debug.Log(pPath + "/" + pCombo.name);
            System.IO.File.Delete(pPath + "/" + pCombo.name + ".asset");
            System.IO.File.Delete(pPath + "/" + pCombo.name + ".asset.meta");
            AssetDatabase.Refresh();
        }

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }
}
