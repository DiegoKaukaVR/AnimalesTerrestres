using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicationAgent : MonoBehaviour
{
    public CommunicationManager communicationManager;

    Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }
    public void RegisterEvents()
    {
        communicationManager.EnemyNotify += OnEnemyNotify;
        communicationManager.ResourceNotify += OnResourceNotify;
    }

    public void UnRegisterEvents()
    {
        communicationManager.EnemyNotify -= OnEnemyNotify;
        communicationManager.ResourceNotify -= OnResourceNotify;
    }
    public void OnEnemyNotify()
    {
        Debug.Log(character.unitName + ": Enemy notified");
    }

    public void OnResourceNotify()
    {
        Debug.Log(character.unitName + ": Resource notified");
    }
}
