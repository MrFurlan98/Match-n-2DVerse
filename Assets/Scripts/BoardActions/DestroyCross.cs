using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCross : BaseAction {

    public DestroyCross()
    {
    }
    public DestroyCross(Vector2Int NxN)
    {
        this.NxN = NxN;
    }
    public override void Action(int pOriginX, int pOriginY, BoardIcon[,] pIcons)
    {
        Vector2Int tNxN = new Vector2Int(NxN.x / 2, NxN.y / 2);
        for(int j=0;j<pIcons.GetLength(1);j++) //
        {
            for (int i = pOriginX - tNxN.x; i <= pOriginX + tNxN.x; i++)
            {
                if (!(i < 0) && i < pIcons.GetLength(0))
                {
                    if (pIcons[i, j].StateIcon != BoardIcon.E_State.CANT_DESTROY && pIcons[i, j] != null)
                        pIcons[i, j].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
                }
            }
        }
        for (int i = 0; i < pIcons.GetLength(0); i++)
        {
            for (int j = pOriginY - tNxN.y; j <= pOriginY + tNxN.y; j++)
            {
                if (!(j < 0) && j < pIcons.GetLength(1))
                {
                    if (pIcons[i, j].StateIcon != BoardIcon.E_State.CANT_DESTROY && pIcons[i, j] != null)
                        pIcons[i, j].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
                }
            }
        }


        /*for (int j = 0; j < pIcons.GetLength(1); j++)
        {
            if (pIcons[pOriginX, j] != null)
            {
                if(pIcons[pOriginX, j].StateIcon != BoardIcon.E_State.CANT_DESTROY)
                    pIcons[pOriginX, j].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
            }
        }
        for (int i = 0; i < pIcons.GetLength(0); i++)
        {
            if (pIcons[i, pOriginY] != null)
            {
                if(pIcons[i, pOriginY].StateIcon != BoardIcon.E_State.CANT_DESTROY)
                     pIcons[i, pOriginY].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
            }
        }*/
    }
}
