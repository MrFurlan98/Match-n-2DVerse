using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class DestroyAllBoardDrawer : ActionDrawer {

    public override void Draw(ref BaseAction pBaseAction)
    {
        GUILayout.Label("Destroy All Board");
    }
}
