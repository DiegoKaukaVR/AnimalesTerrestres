using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class InSightCondition : Conditional
{

    public Character.AnimalType targetSpecie;

    /// <summary>
    ///  LISTA DE CHARACTERS IN VISION
    /// </summary>
    public SharedTransformList inSightTargets;


    public Transform headTransform;
    public float rangeVision;
    public float fieldOfView;

    public LayerMask targetLayer;
    public LayerMask obstacleLayer;

    protected Collider[] hitColliders;
    float dist;
    Vector3 dir;
    float angle;

    public SharedBool hasTarget;

    public override void OnStart()
    {
        inGame = true;
        inSightTargets.Value = new List<Transform>();
    }
    public override TaskStatus OnUpdate()
    {
        if (CheckVisionAI())
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
      
    }

    public bool CheckVisionAI()
    {
        hitColliders = Physics.OverlapSphere(transform.position, rangeVision, targetLayer);
        inSightTargets.Value.Clear();
        if (hitColliders.Length == 0) { return false; }

        for (int i = 0; i < hitColliders.Length; i++)
        {
            dir = hitColliders[i].transform.position - headTransform.position;
            dist = dir.magnitude;

            angle = Vector3.Angle(transform.forward, dir);

            if (dist > rangeVision) { continue; }
            if (angle > fieldOfView / 2) { continue; }
            if (Physics.Raycast(headTransform.position, dir, dist, obstacleLayer)) { continue; }

            if (hitColliders[i].TryGetComponent<Character>(out Character character))
            {
                if (character.dead)
                {
                    continue;
                }
                if (character.specie == targetSpecie)
                {
                    inSightTargets.Value.Add(character.transform);
                }
            
            }
        }

        if (inSightTargets.Value.Count >0)
        {
            hasTarget.Value = true;
            return true;
        }
        else
        {
            hasTarget.Value = false;
            return false;
        }
    }

    bool inGame;
    Vector3 pointA;
    Vector3 pointB;
    Vector3 pointC;
    public override void OnDrawGizmos()
    {
        if (!inGame)
        {
            return;
        }

        Gizmos.color = Color.red;
        pointA = headTransform.position + Quaternion.Euler(0, -fieldOfView / 2, 0) * transform.forward * rangeVision;
        pointB = headTransform.position + Quaternion.Euler(0, fieldOfView / 2, 0) * transform.forward * rangeVision;
        pointC = headTransform.position + transform.forward * rangeVision;
        Gizmos.DrawRay(headTransform.position, transform.forward * rangeVision);
        Gizmos.DrawRay(headTransform.position, Quaternion.Euler(0, -fieldOfView / 2, 0) * transform.forward * rangeVision);
        Gizmos.DrawRay(headTransform.position, Quaternion.Euler(0, fieldOfView / 2, 0) * transform.forward * rangeVision);
        Gizmos.DrawLine(pointA, pointC);
        Gizmos.DrawLine(pointB, pointC);

    }
  
}
