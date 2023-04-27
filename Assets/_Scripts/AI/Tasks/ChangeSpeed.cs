using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ChangeSpeed : Action
{
    public SharedGameObject entityGO;
    [HideInInspector] public IABase entity;

    public float newSpeed = 1f;

    public override void OnAwake()
    {
        entity = entityGO.Value.gameObject.GetComponent<IABase>();
    }

    public override void OnStart()
    {
        entity.myNavmeshAgent.speed = newSpeed;
    }

   
}
