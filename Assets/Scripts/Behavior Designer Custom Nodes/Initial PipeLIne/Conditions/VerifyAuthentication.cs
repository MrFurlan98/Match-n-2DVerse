using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class VerifyAuthentication : Conditional {

    public override TaskStatus OnUpdate()
    {
        return Backend.Instance.WasAuthenticated() ? TaskStatus.Success : TaskStatus.Failure;
    }
}
