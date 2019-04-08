using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class IconEditor : EditorWindow{

    private BaseAction.ACTION_TYPE m_CurrentActionType;

    private Vector2 m_ActionView;

    [MenuItem("Window/ Match N/ Icon Editor")]
    public static void Init(ref Icon pIcon)
    {
        IconEditor tIconEditor = GetWindow<IconEditor>();

        Selection.activeObject = pIcon;

        tIconEditor.minSize = new Vector2(450, 600);
        tIconEditor.maxSize = new Vector2(450, 600);

        tIconEditor.Show();
    }


    private void OnGUI()
    {
        Icon tIcon = Selection.activeObject as Icon;

        if (tIcon == null)
            return;



        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        EditorGUILayout.BeginVertical();

        DrawerSave(ref tIcon);

        DrawSprite(ref tIcon);


        if (tIcon.GetType() == typeof(SpecialIcon))
            DrawSpecialIconInfo(ref tIcon);

        DrawInfo(ref tIcon);

        DrawAddAction(ref tIcon);

        DrawActions(ref tIcon);

        EditorGUILayout.EndVertical();

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();


       
        

    }


    void DrawerSave(ref Icon pIcon)
    {
        if(GUILayout.Button("Save", GUILayout.Width(45))){

            if (pIcon.GetType() == typeof(SpecialIcon))
            {
                SpecialIcon tSpecialIcon = pIcon as SpecialIcon;
                EditorUtility.SetDirty(tSpecialIcon);
            }
            else
                EditorUtility.SetDirty(pIcon);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    void DrawSprite(ref Icon pIcon)
    {
        GUILayout.Space(15);

        pIcon.IconSprite = (Sprite)EditorGUILayout.ObjectField(pIcon.IconSprite, typeof(Sprite), GUILayout.Width(100), GUILayout.Height(100));

        GUILayout.Space(15);
    }

    private void DrawSpecialIconInfo(ref Icon tIcon)
    {
        SpecialIcon tSpecialIcon = (SpecialIcon)tIcon;
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.Label("Amount Generates:");
        tSpecialIcon.MatchValueToGenerate = EditorGUILayout.IntField(tSpecialIcon.MatchValueToGenerate, GUILayout.Width(35));

        GUILayout.FlexibleSpace();

        GUILayout.Label("Generates from Tag:");
        tSpecialIcon.GeneratesTag = EditorGUILayout.TextField(tSpecialIcon.GeneratesTag);

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    void DrawInfo(ref Icon pIcon)
    {
        GUILayout.Space(15);

        GUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();

        GUILayout.Label("Tag Icon:");

        GUILayout.Space(5);

        pIcon.Tag = EditorGUILayout.TextField(pIcon.Tag, GUILayout.Width(250));

        GUILayout.FlexibleSpace();

        GUILayout.EndHorizontal();

        GUILayout.Space(15);
    }

    void DrawAddAction(ref Icon pIcon)
    {
        EditorGUILayout.BeginHorizontal();

        m_CurrentActionType = (BaseAction.ACTION_TYPE) EditorGUILayout.EnumPopup(m_CurrentActionType);

        if (GUILayout.Button("Add Action"))
        {
            if (pIcon.Actions == null)
                pIcon.Actions = new List<Icon.Action>();
            Icon.Action tAction = new Icon.Action();
            tAction.Type = m_CurrentActionType;
            tAction.m_Action = BaseAction.GetActionObject(tAction.Type);
            pIcon.Actions.Add(tAction);
        }

        EditorGUILayout.EndHorizontal();
    }

    void DrawActions(ref Icon pIcon)
    {
        if (pIcon == null || pIcon.Actions == null)
            return;

        EditorGUILayout.BeginVertical("Box", GUILayout.Width(440));
        m_ActionView = EditorGUILayout.BeginScrollView(m_ActionView);
        for (int i = 0; i < pIcon.Actions.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            CustomDrawer(ref pIcon, i);
            if(GUILayout.Button("X", GUILayout.Width(20)))
            {
                pIcon.Actions.RemoveAt(i);

                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }

    void CustomDrawer(ref Icon pIcon, int pIndex)
    {

        Icon.Action tAction = pIcon.Actions[pIndex];

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
}
