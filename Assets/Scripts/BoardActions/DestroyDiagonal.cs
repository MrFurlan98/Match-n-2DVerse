using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDiagonal : BaseAction {

    public override void Action(int pOriginX, int pOriginY, BoardIcon[,] pIcons)
    {
        for (int i = 0; i < (Mathf.Abs(pOriginY - pIcons.GetLength(1))); i++)
        {
            if (pOriginX + i < pIcons.GetLength(0) && pOriginY + i < pIcons.GetLength(1))
            {
                if (pIcons[pOriginX + i, pOriginY + i] != null && pIcons[pOriginX + i, pOriginY + i].StateIcon != BoardIcon.E_State.CANT_DESTROY)
                {
                    pIcons[pOriginX + i, pOriginY + i].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
                }
            }

            if (pOriginX - i >= 0 && pOriginY + i < pIcons.GetLength(1))
            {
                if (pIcons[pOriginX - i, pOriginY + i] != null && pIcons[pOriginX - i, pOriginY + i].StateIcon != BoardIcon.E_State.CANT_DESTROY)
                {
                    pIcons[pOriginX - i, pOriginY + i].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
                }
            }
        }
        for (int i = 0; i <= pOriginY; i++)
        {
            if (pOriginX - i >= 0 && pOriginY - i >= 0)
            {
                if (pIcons[pOriginX - i, pOriginY - i] != null && pIcons[pOriginX - i, pOriginY - i].StateIcon != BoardIcon.E_State.CANT_DESTROY)
                {
                    pIcons[pOriginX - i, pOriginY - i].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
                }
            }
            if (pOriginX + i < pIcons.GetLength(0) && pOriginY - i >= 0)
            {
                if (pIcons[pOriginX + i, pOriginY - i] != null && pIcons[pOriginX + i, pOriginY - i].StateIcon != BoardIcon.E_State.CANT_DESTROY)
                {
                    pIcons[pOriginX + i, pOriginY - i].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
                }
            }
        }
    }
}
