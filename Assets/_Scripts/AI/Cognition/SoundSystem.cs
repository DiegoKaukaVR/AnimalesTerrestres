using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : CognitiveSystem
{
    public float rangeDetection;
    public float hearingAquity = 1f;



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeDetection);
    }
}
