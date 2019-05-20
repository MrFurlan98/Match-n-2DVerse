﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostExplosion : BaseEffect {

    public BoostExplosion()
    {
    }
    public BoostExplosion(string tag)
    {
        this.TargetTag = tag;
    }
    public override void Effect(Board pBoard)
    {
        IconManager.Instance.TransformEffect(TargetTag, pBoard.Heigth, pBoard.Width, pBoard.GetBoardIcons());
    }
}