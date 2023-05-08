using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class InAlertCommunicationCondition : Conditional
{

    public IABase entity;
    public SharedTransformList targets;


    public SharedBool alert;

    public override TaskStatus OnUpdate()
    {
        if (entity.IsAlert())
        {
            if (targets.Value.Capacity == 0)
            {
                targets.Value = new List<Transform>();
            }

            for (int i = 0; i < entity.group.targets.Count; i++)
            {
                if (targets.Value.Contains(entity.group.targets[i]))
                {
                    continue;
                }
                targets.Value.Add(entity.group.targets[i]);
            }
           
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
