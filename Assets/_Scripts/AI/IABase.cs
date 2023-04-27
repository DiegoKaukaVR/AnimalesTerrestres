using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABase : Character
{
    [HideInInspector] public NavMeshAgent myNavmeshAgent;
    [HideInInspector] public Animator animator;

    public Transform Target;
    public bool isPerformingAction;

    protected override void Awake()
    {
        base.Awake();
        myNavmeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
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
        animator.SetTrigger("Attack");
        myNavmeshAgent.ResetPath();
        myNavmeshAgent.velocity = Vector3.zero;
        Debug.Log("Attack");
        
    }
}
