using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CognitiveManager : MonoBehaviour
{
    CognitiveSystem[] cognitiveSystems;
    Character IABase;

    /// <summary>
    /// DATA COLLECTION
    /// </summary>
    public List<Character> adversaries = null;
    public List<Character> alliesTransforms = null;

    private void Start()
    {
        cognitiveSystems = GetComponents<CognitiveSystem>();
        IABase = GetComponentInParent<Character>();
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

        //if (adversaries.Count == 0)
        //{
        //    CommunicationManager.instance.NotifyEnemyInGroup(IABase.groupManager.GetGroupIndex(character.group));
        //}

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
        for (int i = 0; i < adversaries.Count; i++)
        {
            Gizmos.DrawCube(adversaries[i].transform.position, Vector3.one);
        }
    }
}
