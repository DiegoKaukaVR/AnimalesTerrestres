using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


public class SearchNearestTarget : Action
{
    public SharedTransformList targets;


    public override TaskStatus OnUpdate()
    {

        SortByDistance(targets.Value);
        return TaskStatus.Success;
    }

    float dist;
    float minDist;
    int indexMinDist;
    void SortByDistance(List<Transform> listTransform)
    {
        if (listTransform.Count == 0 || listTransform.Count == 1)
        {
            return;
        }

        for (int i = 0; i < listTransform.Count; i++)
        {
            dist = Vector3.Distance(transform.position, listTransform[i].position);
            if (minDist == 0)
            {
                minDist = dist;
                indexMinDist = 0;
            }

            if (dist < minDist)
            {
                minDist = dist;
                indexMinDist = i;
            }
        }

        targets.Value[0] = listTransform[indexMinDist];


    }
}
