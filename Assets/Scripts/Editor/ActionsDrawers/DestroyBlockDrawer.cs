using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class DestroyBlockDrawer : ActionDrawer {

    public override void Draw(ref BaseAction pBaseAction)
    {
        GUILayout.Space(15);

        GUILayout.BeginHorizontal("Box");

        GUILayout.Label("Destroy Block");
        pBaseAction.NxN = EditorGUILayout.Vector2IntField("NxN", pBaseAction.NxN);

        GUILayout.EndHorizontal();

        

        GUILayout.Space(15);
    }
}
