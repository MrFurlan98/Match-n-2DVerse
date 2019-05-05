using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSuper : BaseEffect {

    public BoostSuper()
    {
    }
    public BoostSuper(string tag)
    {
        this.TargetTag = tag;
    }
    public override void Effect(Board pBoard)
    {
        pBoard.BoostSuper = true;
    }
}
