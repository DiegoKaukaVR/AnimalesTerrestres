using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicationAgent : MonoBehaviour
{
    public CommunicationManager communicationManager;
    SimpleCommunication simpleCommunication;

    Character character;

    int groupIndex;

    private void Awake()
    {
        character = GetComponent<Character>();
        simpleCommunication = GetComponent<SimpleCommunication>();
    }
    private void OnEnable()
    {
        simpleCommunication.OnCommunicationEvent += ReceiveMessage;
    }

    private void OnDisable()
    {
        
    }

    void ReceiveMessage()
    {
        Debug.Log("Mensaje Recibido");
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
