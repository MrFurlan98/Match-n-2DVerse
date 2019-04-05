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
        Debug.Log("entrou");
        Vector2Int tNxN = new Vector2Int(NxN.x / 2, NxN.y / 2);
        for (int i = pOriginX-tNxN.x; i < pOriginX + tNxN.x && pIcons.GetLength(0)>= pOriginX + tNxN.x; i++)
        {
            for (int j = pOriginY-(NxN.y/2); j < pOriginY + (NxN.y / 2) && pIcons.GetLength(1)>= pOriginY + (NxN.y / 2); j++)
            {
                if (pIcons[i, j].StateIcon != BoardIcon.E_State.CANT_DESTROY)
                    pIcons[i, j].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
            }
        }
    }
}
