using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollumn : MonoBehaviour, IBoardAction
{
    public void Action(int pOriginX, int pOriginY, Icon[,] pIcons)
    {
        for (int i = 0; i < pIcons.GetLength(1); i++)
        {
            if (pIcons[pOriginX, i] != null && pIcons[pOriginX, i].StateIcon != Icon.E_State.CANT_DESTROY)
            {
                pIcons[pOriginX, i].StateIcon = Icon.E_State.MARK_TO_DESTROY;
            }
        }
    }
}
