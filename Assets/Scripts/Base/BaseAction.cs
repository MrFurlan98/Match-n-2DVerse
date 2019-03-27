using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseAction : IBoardAction{

    public enum ACTION_TYPE
    {
        DESTROY_CROSS,
        DESTROY_BLOCK,
        DESTROY_BY_TYPE
    }

    public static BaseAction Actions(ACTION_TYPE pActionType)
    {
       switch(pActionType)
        {
            case ACTION_TYPE.DESTROY_BLOCK:
                // do stuff
                break;
            case ACTION_TYPE.DESTROY_BY_TYPE:
                return new DestroyByTag();
        }
        return new BaseAction();
    }

    public virtual void Action(int pOriginX, int pOriginY, Icon[,] pIcons)
    {
        throw new System.NotImplementedException();
    }
}
