using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllBoard : BaseAction
{
    public DestroyAllBoard()
    {
    }

    public override void Action(int pOriginX, int pOriginY, BoardIcon[,] pIcons)
    {
        for (int i = 0; i < pIcons.GetLength(0); i++)
        {
            for (int j = 0; j < pIcons.GetLength(1); j++)
            {
                if(pIcons[i,j]!=null)
                {
                    if (pIcons[i, j].StateIcon != BoardIcon.E_State.CANT_DESTROY && pIcons[i, j].StateIcon != BoardIcon.E_State.CANT_DESTROY)
                        pIcons[i, j].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
                }   
            }
        }
    }
}
