using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;
public class MoveTargetTask : Action
{
    public float newSpeed = 1f;
    public SharedTransformList targets;

    public SharedGameObject entityGO;
    [HideInInspector] public IABase entity;

    bool inGame;

    public override void OnStart()
    {
        entity = entityGO.Value.gameObject.GetComponent<IABase>();
        inGame = true;
        entity.ChangeSpeed(newSpeed);
    }

    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(transform.position, targets.Value[0].position) < 0.1f)
        {
            return TaskStatus.Success;
        }

        if (Time.frameCount % 10 == 0)
        {
            entity.GoTarget(targets.Value[0].position);
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
    public override void OnDrawGizmos()
    {
        if (!inGame)
        {
            return;
        }
        //Gizmos.DrawCube(targets.Value[0].position, Vector3.one * 0.1f);
    }


}
    

