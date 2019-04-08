using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDiagonalDrawer : ActionDrawer {

    public override void Draw(ref BaseAction pBaseAction)
    {
        GUILayout.Space(15);

        GUILayout.BeginHorizontal("Box");

        GUILayout.Label("Destroy Diagonal");

        GUILayout.EndHorizontal();

        GUILayout.Space(15);
    }
}
