using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DestroyByTypeEffectDrawer : EffectDrawer {
    public override void Draw(ref BaseEffect pBaseEffect)
    {
        GUILayout.Space(15);

        GUILayout.BeginHorizontal("Box");

        GUILayout.Label("Tag");

        GUILayout.Space(5);

        pBaseEffect.TargetTag = EditorGUILayout.TextField(pBaseEffect.TargetTag, GUILayout.Width(250));

        GUILayout.EndHorizontal();

        GUILayout.Space(15);
    }
}
