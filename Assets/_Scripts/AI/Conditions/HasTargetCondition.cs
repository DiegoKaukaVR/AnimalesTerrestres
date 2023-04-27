using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HasTargetCondition : Conditional
{
    public SharedBool hasTarget;

    public override TaskStatus OnUpdate()
    {
        if (hasTarget.Value)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
