using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ActionDrawer {

    public virtual void Draw(ref BaseAction pBaseAction)
    {
        // write the current action ability draw ui here in derivateds class
    }

    public static void DrawTypeList(ref BaseAction pBaseAction, BaseAction.ACTION_TYPE pActionType)
    { 
        switch (pActionType)
        {
            case BaseAction.ACTION_TYPE.DESTROY_BY_TYPE:
                DestroyByTagDrawer tDestroyByTagDrawer = new DestroyByTagDrawer();

                tDestroyByTagDrawer.Draw(ref pBaseAction);
                break;
            case BaseAction.ACTION_TYPE.DESTROY_CROSS:
                DestroyCrossDrawer tDestroyCrossDrawer = new DestroyCrossDrawer();

                tDestroyCrossDrawer.Draw(ref pBaseAction);
                break;
            case BaseAction.ACTION_TYPE.DESTROY_ALL_BOARD:
                DestroyAllBoardDrawer tDestroyAllBoardDrawer = new DestroyAllBoardDrawer();

                tDestroyAllBoardDrawer.Draw(ref pBaseAction);
                break;


        }
    }
}
