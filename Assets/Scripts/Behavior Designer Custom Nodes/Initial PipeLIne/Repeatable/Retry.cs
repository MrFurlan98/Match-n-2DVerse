
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
public class Retry : Repeater {

    private int executionCount;
    private TaskStatus executionStatus;
    public override bool CanExecute()
    {
        // Continue executing until we've reached the count or the child task returned failure and we should stop on a failure.
        return (repeatForever.Value || executionCount < count.Value) && (!endOnSuccess.Value || (endOnSuccess.Value && executionStatus != TaskStatus.Success) && (!endOnFailure.Value || (endOnFailure.Value && executionStatus != TaskStatus.Failure)));
    }

    public override void OnChildExecuted(TaskStatus childStatus)
    {
        // The child task has finished execution. Increase the execution count and update the execution status.
        executionCount++;
        executionStatus = childStatus;
    }

    public override void OnEnd()
    {
        // Reset the variables back to their starting values.
        executionCount = 0;
        executionStatus = TaskStatus.Inactive;
    }

    public override void OnReset()
    {
        // Reset the public properties back to their original values.
        count = 0;
        endOnFailure = true;
    }
}
