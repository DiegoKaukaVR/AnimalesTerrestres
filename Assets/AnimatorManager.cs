using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    IABase entity;
    Animator animator;

    private void Start()
    {
        entity = GetComponentInParent<IABase>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!entity.isPerformingAction)
        {
            UpdateXZVelocity();
        }
        else
        {
            ResetXZVelocity();
        }
    }

    Vector3 interpolatedVelocity;

    public void ResetXZVelocity()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(entity.myNavmeshAgent.velocity);

        interpolatedVelocity = Vector3.Lerp(interpolatedVelocity, Vector3.zero, Time.deltaTime);

        animator.SetFloat("XSpeed", interpolatedVelocity.x);
        animator.SetFloat("ZSpeed", interpolatedVelocity.y);
    }
    /// <summary>
    /// Updates XZvelocity from navmeshagent to Animator parameters used in Blendtree movement. 
    /// </summary>
    public virtual void UpdateXZVelocity()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(entity.myNavmeshAgent.velocity);
        
        if (localVelocity.sqrMagnitude > 0.2f)
            interpolatedVelocity = Vector3.Lerp(interpolatedVelocity, localVelocity, 0.25f);
        else
            interpolatedVelocity = Vector3.Lerp(interpolatedVelocity, localVelocity, 0.1f);

        animator.SetFloat("XSpeed", interpolatedVelocity.x);
        animator.SetFloat("ZSpeed", interpolatedVelocity.z);
        return;
    }


    public void AttackOut()
    {
        entity.AttackOut();
    }



}
