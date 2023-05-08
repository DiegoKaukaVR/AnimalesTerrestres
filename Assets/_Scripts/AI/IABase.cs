using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner;
using BehaviorDesigner.Runtime;

public class IABase : Character
{
    [HideInInspector] public NavMeshAgent myNavmeshAgent;
    BehaviorTree behaviorTree;

    public Collider colliderAttack;

    public Transform Target;
    public bool isPerformingAction;

    protected override void Awake()
    {
        base.Awake();
        myNavmeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        behaviorTree = GetComponent<BehaviorTree>();
        group = null;

    }

   




    public void GoTarget()
    {
        myNavmeshAgent.SetDestination(Target.position);
    }
    public void GoTarget(Transform newTarget)
    {
        Target = newTarget;
        myNavmeshAgent.SetDestination(newTarget.position);
    }
    public void GoTarget(Vector3 position)
    {
        myNavmeshAgent.SetDestination(position);
    }


    public void Attack()
    {
        isPerformingAction = true;
        animator.SetTrigger("Attack");
        colliderAttack.enabled = true;
        //myNavmeshAgent.ResetPath();
        //myNavmeshAgent.velocity = Vector3.zero;
        //colliderAttack.enabled = true;
        Debug.Log("Attack");
    }

    public void AttackOut()
    {
        Debug.Log("AttackOut");
        colliderAttack.enabled = false;
        isPerformingAction = false;
    }

    public void ChangeSpeed(float newSpeed)
    {
        myNavmeshAgent.speed = newSpeed;
    }

    public override void Death()
    {
        base.Death();
        animator.SetTrigger("Death");
        animator.applyRootMotion = true;
        myNavmeshAgent.velocity = Vector3.zero;
        myNavmeshAgent.enabled = false;
        behaviorTree.DisableBehavior();
        this.enabled = false;
       
    }
}
