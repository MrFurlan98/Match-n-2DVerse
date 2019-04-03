using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCross : BaseAction {

    public override void Action(int pOriginX, int pOriginY, BoardIcon[,] pIcons)
    {
        for (int i = 0; i < pIcons.GetLength(1); i++)
        {
            if (pIcons[pOriginX, i] != null && pIcons[pOriginX, i].StateIcon != BoardIcon.E_State.CANT_DESTROY)
            {
                pIcons[pOriginX, i].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
            }
        }
        for (int i = 0; i < pIcons.GetLength(0); i++)
        {
            if (pIcons[i, pOriginY] != null && pIcons[i, pOriginY].StateIcon != BoardIcon.E_State.CANT_DESTROY)
            {
                pIcons[i, pOriginY].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
            }
        }
    }
}
