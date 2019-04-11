using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TransformIntoSpecialDrawer : ActionDrawer
{

    public override void Draw(ref BaseAction pBaseAction)
    {
        GUILayout.Space(15);

        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal("Box");

        GUILayout.Label("Tag To Transform");

        GUILayout.Space(5);

        pBaseAction.TargetTag = EditorGUILayout.TextField(pBaseAction.TargetTag, GUILayout.Width(250));

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("Box");

        GUILayout.Label("Tag To Transform To");

        GUILayout.Space(5);

        pBaseAction.TagToTransformTo = EditorGUILayout.TextField(pBaseAction.TagToTransformTo, GUILayout.Width(250));

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUILayout.Space(15);
    }
}
