using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class FleeTarget : Action
{
    public SharedGameObject entityGO;
    [HideInInspector] public IABase entity;

    public float newSpeed = 1f;


    public SharedTransformList targets;

    public override void OnAwake()
    {
        entity = entityGO.Value.gameObject.GetComponent<IABase>();
    }

    public override void OnStart()
    {
        entity.myNavmeshAgent.speed = newSpeed;
     
    }
    Vector3 dir;
    Vector3 pos;
    public override TaskStatus OnUpdate()
    {
        if (Time.frameCount % 10 == 0)
        {
            /// Calcular dirección opuesta al Target
            dir = transform.position - targets.Value[0].position;

            /// Calcular punto 
            pos = transform.position + (dir.normalized * 2f);

            /// Aplicar go to target
            entity.GoTarget(pos);
        }
        return TaskStatus.Running;
    }
}
