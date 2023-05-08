using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;


public class AttackTarget : Action
{
    public SharedTransformList targets;
    public SharedBool hasTarget;
    public float minDistAttack = 1f;
    public SharedGameObject entityGO;

    bool canAttack = true;
    float cooldownAttack;

    IABase entity;

    public override void OnAwake()
    {
        entity = entityGO.Value.GetComponent<IABase>();
    }
    public override TaskStatus OnUpdate()
    {
        if (hasTarget.Value == false)
        {
            return TaskStatus.Failure;
        }

      
        if (targets.Value[0].GetComponent<Character>().dead)
        {
            hasTarget.Value = false;
            return TaskStatus.Failure;
        }
        if (entity.isPerformingAction)
        {
            return TaskStatus.Success;
        }
        if (canAttack)
        {
            if (Vector3.Distance(transform.position, targets.Value[0].position)< minDistAttack)
            {
                canAttack = false;
                entity.Attack();
                StartCoroutine(CooldownAttack());
                return TaskStatus.Success;
            }
        }

        return TaskStatus.Failure;

    }

    IEnumerator CooldownAttack()
    {
        yield return new WaitForSecondsRealtime(cooldownAttack);
        canAttack = true;
    }
}
