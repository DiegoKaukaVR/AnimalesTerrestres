using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSystem : CognitiveSystem
{

    public Transform headTransform;
    public float rangeVision;
    public float fieldOfView;

    public LayerMask targetLayer;
    public LayerMask obstacleLayer;

    protected Collider[] hitColliders;
    float dist;
    Vector3 dir;
    float angle;

    public override void TickCongition()
    {
        CheckVisionAI();
    }

    public void CheckVisionAI()
    {
        hitColliders = Physics.OverlapSphere(transform.position, rangeVision, targetLayer);
        cognitiveManager.CleanAdversary();
        if (hitColliders.Length == 0) { return; }

        for (int i = 0; i < hitColliders.Length; i++)
        {
            dir = hitColliders[i].transform.position - headTransform.position;
            dist = dir.magnitude;

            angle = Vector3.Angle(transform.forward, dir);

            if (dist > rangeVision) { continue; }
            if (angle > fieldOfView / 2) { continue; }
            if (Physics.Raycast(headTransform.position, dir, dist, obstacleLayer)) { continue;}

            if (hitColliders[i].TryGetComponent<Character>(out Character character))
            {
                cognitiveManager.AddAdversary(character);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(headTransform.position, Quaternion.Euler(0, -fieldOfView/2, 0) * transform.forward * rangeVision);
        Gizmos.DrawRay(headTransform.position, Quaternion.Euler(0, fieldOfView/2, 0) * transform.forward * rangeVision);
    }
}

