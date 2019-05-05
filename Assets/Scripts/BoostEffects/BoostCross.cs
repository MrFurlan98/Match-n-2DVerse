using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostCross : BaseEffect {

    public BoostCross()
    {
    }
    public BoostCross(string tag)
    {
        this.TargetTag = tag;
    }
    public override void Effect(Board pBoard)
    {
        pBoard.BoostCross = true;
    }
}
