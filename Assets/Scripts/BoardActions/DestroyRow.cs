﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRow : MonoBehaviour, IBoardAction
{
    public void Action(int pOriginX, int pOriginY, BoardIcon[,] pIcons)
    {
        for (int i = 0; i < pIcons.GetLength(0); i++)
        {
            if(pIcons[i, pOriginY] != null && pIcons[i, pOriginY].StateIcon != BoardIcon.E_State.CANT_DESTROY)
            {
                pIcons[i, pOriginY].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
            }
        }
    }
}
