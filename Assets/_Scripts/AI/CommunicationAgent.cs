using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicationAgent : MonoBehaviour
{
    public CommunicationManager communicationManager;

    Character character;

    int groupIndex;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    void GetGroupIndex()
    {
        groupIndex = character.groupManager.GetGroupIndex(character.group);
    }
    public void RegisterEvents()
    {
        GetGroupIndex();
        communicationManager.enemyNotifyList[groupIndex] += OnEnemyNotify;
        communicationManager.enemyNotifyList[groupIndex] += OnResourceNotify;
    }

    public void UnRegisterEvents()
    {
        GetGroupIndex();
        communicationManager.enemyNotifyList[groupIndex] -= OnEnemyNotify;
        communicationManager.enemyNotifyList[groupIndex] -= OnResourceNotify;
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
