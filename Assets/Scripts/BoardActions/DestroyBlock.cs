using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlock : BaseAction {

    public DestroyBlock()
    {
    }
    public DestroyBlock(Vector2Int NxN)
    {
        this.NxN = NxN;
    }
    public override void Action(int pOriginX, int pOriginY, BoardIcon[,] pIcons)
    {
        Vector2Int tNxN = new Vector2Int(NxN.x / 2, NxN.y / 2);
        for (int i = pOriginX-tNxN.x; i <= pOriginX + tNxN.x; i++)
        {
            if(!(i<0) && i<pIcons.GetLength(0))
            {
                for (int j = pOriginY - tNxN.y; j <= pOriginY + tNxN.y; j++)
                {
                    if (!(j < 0) && j < pIcons.GetLength(1))
                    {
                        if (pIcons[i, j].StateIcon != BoardIcon.E_State.CANT_DESTROY && pIcons[i,j]!=null)
                            pIcons[i, j].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
                    }
                }
            }  
        }
    }
}
