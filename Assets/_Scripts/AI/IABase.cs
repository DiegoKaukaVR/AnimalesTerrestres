using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABase : Character
{
    [HideInInspector] public NavMeshAgent myNavmeshAgent;

    public Transform Target;
    public bool isPerformingAction;

    protected override void Awake()
    {
        base.Awake();
        myNavmeshAgent = GetComponent<NavMeshAgent>();
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
}
