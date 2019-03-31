using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class DestroyByTagDrawer : ActionDrawer {

    public override void Draw(ref BaseAction pBaseAction)
    {
        GUILayout.Space(15);

        GUILayout.BeginHorizontal("Box");

        GUILayout.Label("Tag To Destroy");

        GUILayout.Space(5);

        pBaseAction.TargetTag = EditorGUILayout.TextField(pBaseAction.TargetTag, GUILayout.Width(250));

        GUILayout.EndHorizontal();

        GUILayout.Space(15);
    }
}
