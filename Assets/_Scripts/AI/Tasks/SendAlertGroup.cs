using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class SendAlertGroup : Action
{
    public Character entity;
    public SharedTransformList targets;

    public override void OnStart()
    {
        if (entity.HasGroup())
        {
            entity.SendAlertMessage();
            entity.SendEnemyTransforms(targets.Value);
        }
     
    }
}
