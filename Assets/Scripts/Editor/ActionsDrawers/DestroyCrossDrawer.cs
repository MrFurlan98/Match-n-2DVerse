using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCrossDrawer : ActionDrawer {

    public override void Draw(ref BaseAction pBaseAction)
    {
        GUILayout.Space(15);

        GUILayout.BeginHorizontal("Box");

        GUILayout.Label("Destroy Cross");

        GUILayout.EndHorizontal();

        GUILayout.Space(15);
    }
}
