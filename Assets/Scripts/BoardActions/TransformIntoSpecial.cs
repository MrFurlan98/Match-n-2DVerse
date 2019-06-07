using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransformIntoSpecial : BaseAction
{
    public TransformIntoSpecial()
    {
    }
    public TransformIntoSpecial(string TargetTag, string TagToTransformTo)
    {
        this.TargetTag = TargetTag;
        this.TagToTransformTo = TagToTransformTo;
    }
    public override void Action(int pOriginX, int pOriginY, BoardIcon[,] pIcons)
    {
        for (int i = 0; i < pIcons.GetLength(0); i++)
        {
            for (int j = 0; j < pIcons.GetLength(1); j++)
            {
                if (pIcons[i, j].STag == TargetTag && pIcons[i, j].StateIcon != BoardIcon.E_State.CANT_DESTROY && pIcons[i, j].StateIcon != BoardIcon.E_State.OBJECTIVE)
                {
                    Icon tIcon = IconManager.Instance.TransformIcon(i, j, TagToTransformTo);
                    if(tIcon!=null)
                    {
                        pIcons[i, j].SetBoardData(tIcon);
                        pIcons[i, j].Type = BoardIcon.E_Type.SPECIAL;
                    }
                    pIcons[i, j].StateIcon = BoardIcon.E_State.MARK_TO_DESTROY;
                }
            }
        }
    }
}
