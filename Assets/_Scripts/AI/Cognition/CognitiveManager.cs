using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CognitiveManager : MonoBehaviour
{
    CognitiveSystem[] cognitiveSystems;

    /// <summary>
    /// DATA COLLECTION
    /// </summary>
    public List<Character> adversaries = null;
    public List<Character> alliesTransforms = null;

    private void Start()
    {
        cognitiveSystems = GetComponents<CognitiveSystem>();
    }
    private void Update()
    {
        CheckCognition();
    }
    public void AddAdversary(Character character)
    {
        if (adversaries.Contains(character))
        {
            return;
        }

        adversaries.Add(character);
    }

    public void CleanAdversary()
    {
        adversaries.Clear();
    }
    public void RemoveAdversary(Character character)
    {
        if (adversaries.Contains(character))
        {
            return;
        }

        adversaries.Remove(character);
    }
    void CheckCognition()
    {
        for (int i = 0; i < cognitiveSystems.Length; i++)
        {
            cognitiveSystems[i].TickCongition();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (adversaries.Count == 0)
        {
            return;
        }
        for (int i = 0; i < adversaries.Count; i++)
        {
            Gizmos.DrawLine(transform.position, adversaries[i].transform.position);
            Gizmos.DrawCube(adversaries[i].transform.position, Vector3.one);
        }
    }
}
