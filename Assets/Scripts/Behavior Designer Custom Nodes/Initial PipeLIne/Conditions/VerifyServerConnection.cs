using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class VerifyServerConnection : Conditional {

    public override TaskStatus OnUpdate()
    {
        if (Backend.Instance.ServerAvailable())
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
}
