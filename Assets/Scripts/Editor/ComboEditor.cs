using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ComboEditor : EditorWindow{

    private static ComboEditor m_ComboEditor;

    private Vector2 m_ActionView;

    private BaseAction.ACTION_TYPE m_CurrentActionType;

    [MenuItem("Window/ Match N/ Combo Editor")]
    static void INIT()
    {
        m_ComboEditor = GetWindow<ComboEditor>();
        m_ComboEditor.Show();
    }

    private void OnGUI()
    {

        Combo tCombo = Selection.activeObject as Combo;

        if (tCombo == null)
            return;

        DrawerSave(ref tCombo);

        EditorGUILayout.BeginHorizontal();

        tCombo.Icon1 = (SpecialIcon)EditorGUILayout.ObjectField(tCombo.Icon1, typeof(SpecialIcon));

        tCombo.Icon2 = (SpecialIcon)EditorGUILayout.ObjectField(tCombo.Icon2, typeof(SpecialIcon));

        EditorGUILayout.EndHorizontal();

  

        EditorGUILayout.BeginHorizontal();
        //Draw left icon info
        EditorGUILayout.BeginVertical();
        if (tCombo.Icon1 != null) {
            DrawSpecialIconInfo(tCombo.Icon1);
        }
        EditorGUILayout.EndVertical();

        //Draw right icon info
        EditorGUILayout.BeginVertical();

        if (tCombo.Icon2 != null)
        {
            DrawSpecialIconInfo(tCombo.Icon2);
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();


        DrawAddAction(ref tCombo);

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        DrawActions(ref tCombo);

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        
    }

    void DrawSpecialIconInfo(SpecialIcon pSpecialIcon)
    {
        Texture2D tTexture = pSpecialIcon.IconSprite.texture;

        GUILayout.Label(tTexture, GUILayout.Width(50), GUILayout.Height(50));

    }

    void DrawerSave(ref Combo pCombo)
    {
        if (GUILayout.Button("Save", GUILayout.Width(45)))
        {
            EditorUtility.SetDirty(pCombo);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    void DrawAddAction(ref Combo pCombo)
    {
        EditorGUILayout.BeginHorizontal();

        m_CurrentActionType = (BaseAction.ACTION_TYPE)EditorGUILayout.EnumPopup(m_CurrentActionType);

        if (GUILayout.Button("Add Action"))
        {
            if (pCombo.Actions == null)
                pCombo.Actions = new List<Icon.Action>();
            Icon.Action tAction = new Icon.Action();
            tAction.Type = m_CurrentActionType;
            tAction.m_Action = BaseAction.GetActionObject(tAction.Type);
            pCombo.Actions.Add(tAction);
        }

        EditorGUILayout.EndHorizontal();
    }

    void DrawActions(ref Combo pCombo)
    {
        if (pCombo.Actions == null)
            return;

        EditorGUILayout.BeginVertical("Box", GUILayout.Width(440));
        m_ActionView = EditorGUILayout.BeginScrollView(m_ActionView);
        for (int i = 0; i < pCombo.Actions.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            CustomDrawer(ref pCombo, i);
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                pCombo.Actions.RemoveAt(i);

                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }

    void CustomDrawer(ref Combo pCombo, int pIndex)
    {

        Icon.Action tAction = pCombo.Actions[pIndex];

        BaseAction tBaseAction = null;
        if (tAction.ActionToRun == null)
        {
            tBaseAction = BaseAction.GetActionObject(tAction.Type);
        }
        else
        {
            tBaseAction = tAction.ActionToRun;
        }
        ActionDrawer.DrawTypeList(ref tBaseAction, tAction.m_Type);
    }
    void DrawPowerUp(ref Combo pCombo, int tIndex)
    {
        Icon.Action tAction = pCombo.Actions[tIndex];

        BaseAction
    }
}
