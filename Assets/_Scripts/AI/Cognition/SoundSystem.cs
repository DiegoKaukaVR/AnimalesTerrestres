using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : CognitiveSystem
{
    public float rangeDetection;
    public float hearingAquity = 1f;

    public float minFreq = 200;
    public float maxFreq = 18000;

    [SerializeField] LayerMask targetLayer;
    Collider[] colliderArray;
    List<SoundEmitter> sounds = new List<SoundEmitter>();
    List<SoundEmitter> finalSounds = new List<SoundEmitter>();

    public override void TickCongition()
    {
        CheckSoundAI();
    }

    void CheckSoundAI()
    {
        colliderArray = Physics.OverlapSphere(transform.position, rangeDetection, targetLayer);
        sounds.Clear();
        finalSounds.Clear();

        for (int i = 0; i < colliderArray.Length; i++)
        {
            if (colliderArray[i].TryGetComponent<SoundEmitter>(out SoundEmitter sound))
            {
                sounds.Add(sound);
            }
        }

        SoundDetection();
    }

    void SoundDetection()
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            // Freq filter
            if (sounds[i].soundFrecuency < minFreq && sounds[i].soundFrecuency > maxFreq)
            {
                continue;
            }

            float dist = Vector3.Distance(transform.position, sounds[i].transform.position);

            // Intensity filter
            if (sounds[i].soundIntensity < dist / hearingAquity)
            {
                continue;
            }

            finalSounds.Add(sounds[i]);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeDetection);

        if (finalSounds.Count > 0)
        {
            for (int i = 0; i < finalSounds.Count; i++)
            {
                Gizmos.DrawSphere(finalSounds[i].transform.position, 0.6f);
            }
        }
    }
}
