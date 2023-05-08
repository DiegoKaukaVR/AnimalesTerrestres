using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class WanderMove : Action
{

    public SharedGameObject entityGO;
    public IABase entity;
    public float newSpeed = 2f;

    Vector3 randomPos;

    float timer;
    float currentObjetive;
    public float maxRange = 5f;
    public float minTime = 5f;
    public float maxTime = 10f;
    bool game;

    public override void OnAwake()
    {
        entityGO.Value.GetComponent<IABase>();
        game = true;
        entity.ChangeSpeed(newSpeed);
        CalculateRandomTimer();
        GoRandomPoint();
    }


   
    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(transform.position, randomPos) < 1.9f)
        {
            GoRandomPoint();
            timer = 0;
        }
        if (timer   >= currentObjetive)
        {
            timer = 0;
            CalculateRandomTimer();
            GoRandomPoint();
        }
        else
        {
            timer += Time.deltaTime;
        }

        return TaskStatus.Success;
        

    }

    private void GoRandomPoint()
    {
        CalculateRandomPos();
        entity.GoTarget(randomPos);
    }

   
    void CalculateRandomPos()
    {
        randomPos = new Vector3(Random.Range(1, maxRange), Random.Range(1, maxRange), Random.Range(1, maxRange));
    }

    void CalculateRandomTimer()
    {
        currentObjetive = Random.Range(minTime, maxTime);
    }

    public override void OnDrawGizmos()
    {
        if (!game)
        {
            return;
        }
        //Gizmos.DrawLine(transform.position, randomPos);
        //Gizmos.DrawSphere(randomPos, 0.2f);
    }

}
