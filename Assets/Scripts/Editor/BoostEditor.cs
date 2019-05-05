using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class BoostEditor : EditorWindow {

    private BaseEffect.EFFECT_TYPE m_CurrentEffectType;

    private Vector2 m_ActionView;

    [MenuItem("Window/ Match N/ Boost Editor")]
    public static void Init(ref Boost pBoost)
    {
        BoostEditor tBoostEditor = GetWindow<BoostEditor>();

        Selection.activeObject = pBoost;

        tBoostEditor.minSize = new Vector2(450, 600);
        tBoostEditor.maxSize = new Vector2(450, 600);

        tBoostEditor.Show();
    }
    private void OnGUI()
    {
        Boost tBoost = Selection.activeObject as Boost;

        if (tBoost == null)
            return;



        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        EditorGUILayout.BeginVertical();

        DrawerSave(ref tBoost);

        DrawSprite(ref tBoost);


        DrawInfo(ref tBoost);

        DrawAddEffect(ref tBoost);

        DrawEffects(ref tBoost);

        EditorGUILayout.EndVertical();

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();

    }

    void DrawerSave(ref Boost pBoost)
    {
        if (GUILayout.Button("Save", GUILayout.Width(45)))
        {
            EditorUtility.SetDirty(pBoost);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    void DrawSprite(ref Boost pBoost)
    {
        GUILayout.Space(15);

        pBoost.BoostSprite = (Sprite)EditorGUILayout.ObjectField(pBoost.BoostSprite, typeof(Sprite), GUILayout.Width(100), GUILayout.Height(100));

        GUILayout.Space(15);
    }

    void DrawInfo(ref Boost pBoost)
    {
        GUILayout.Space(15);

        GUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();

        GUILayout.Label("Tag Boost:");

        GUILayout.Space(5);

        pBoost.Tag = EditorGUILayout.TextField(pBoost.Tag, GUILayout.Width(250));

        GUILayout.FlexibleSpace();

        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        GUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();

        GUILayout.Label("Qtd:");

        GUILayout.Space(5);

        pBoost.Qtd = EditorGUILayout.IntField(pBoost.Qtd, GUILayout.Width(250));

        GUILayout.FlexibleSpace();

        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        GUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();

        GUILayout.Label("Value To Buy:");

        GUILayout.Space(5);

        pBoost.ValueToBuy = EditorGUILayout.IntField(pBoost.ValueToBuy, GUILayout.Width(250));

        GUILayout.FlexibleSpace();

        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        GUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();

        GUILayout.Label("Time in Seconds:");

        GUILayout.Space(5);

        pBoost.TimeLimit = EditorGUILayout.FloatField(pBoost.TimeLimit, GUILayout.Width(250));

        GUILayout.FlexibleSpace();

        GUILayout.EndHorizontal();

        GUILayout.Space(15);
    }

    void DrawAddEffect(ref Boost pBoost)
    {
        EditorGUILayout.BeginHorizontal();

        m_CurrentEffectType = (BaseEffect.EFFECT_TYPE)EditorGUILayout.EnumPopup(m_CurrentEffectType);

        if (GUILayout.Button("Add Effect"))
        {
            if (pBoost.Effects == null)
                pBoost.Effects = new List<Boost.Effect>();
            Boost.Effect tEffect = new Boost.Effect();
            tEffect.Type = m_CurrentEffectType;
            tEffect.m_Effect = BaseEffect.GetEffectObject(tEffect.Type);
            pBoost.Effects.Add(tEffect);
        }

        EditorGUILayout.EndHorizontal();
    }
    void DrawEffects(ref Boost pBoost)
    {
        if (pBoost == null || pBoost.Effects == null)
            return;

        EditorGUILayout.BeginVertical("Box", GUILayout.Width(440));
        m_ActionView = EditorGUILayout.BeginScrollView(m_ActionView);
        for (int i = 0; i < pBoost.Effects.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            CustomDrawer(ref pBoost, i);
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                pBoost.Effects.RemoveAt(i);

                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }
    void CustomDrawer(ref Boost pBoost, int pIndex)
    {

        Boost.Effect tEffect = pBoost.Effects[pIndex];

        BaseEffect tBaseEffect = null;
        if (tEffect.EffectToRun == null)
        {
            tBaseEffect = BaseEffect.GetEffectObject(tEffect.Type);
        }
        else
        {
            tBaseEffect = tEffect.EffectToRun;
        }
        EffectDrawer.DrawTypeList(ref tBaseEffect, tEffect.Type);
    }
}
