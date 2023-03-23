using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;



public class MoveTarget : Action
{
    public float speed = 1f;
    public SharedTransform target;
    public SharedGameObject entityGO;
    [HideInInspector] public IABase entity;

    public override void OnStart()
    {
        entity = entityGO.Value.gameObject.GetComponent<IABase>();
    }

    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(transform.position, target.Value.position)<0.1f)
        {
            return TaskStatus.Success;
        }

        if (Time.frameCount % 10 == 0)
        {
            entity.GoTarget(target.Value.position);
        }

        return TaskStatus.Running;
    }

}
