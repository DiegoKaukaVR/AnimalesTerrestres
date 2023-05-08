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

        count = maxCount;

    }
    private void OnEnable()
    {
        RegisterEvents();
    }

    private void OnDisable()
    {
        UnRegisterEvents();
    }

    public void SendAlertMessageGroup()
    {
        GetGroupIndex();
        Debug.Log("Estar alerta grupo!!");
        communicationManager.enemyNotifyList[groupIndex].Invoke();
            
    }

    int count;
    int maxCount = 100000;

    bool onlyOnce;
    public void SendEnemiesTargets(List<Transform> targets)
    {
        Debug.Log("Estas son las posiciones de los enemigos!!");
        if (character.group != null && character.group.alliesInGroup.Count >0)
        {
            character.group.targets = targets;
        }

    }


    void GetGroupIndex()
    {
        groupIndex = character.groupManager.GetGroupIndex(character.group);
    }


    public void RegisterEvents()
    {
        GetGroupIndex();
        communicationManager.enemyNotifyList[groupIndex] += character.ReceiveAlertMessage;
        //communicationManager.enemyNotifyPositions[groupIndex] += character.SendEnemyTransforms;


        //communicationManager.enemyNotifyList[groupIndex] += OnResourceNotify;
    }

    public void UnRegisterEvents()
    {
        GetGroupIndex();
        communicationManager.enemyNotifyList[groupIndex] -= character.ReceiveAlertMessage;
        //communicationManager.enemyNotifyPositions[groupIndex] -= character.SendEnemyTransforms;
        //communicationManager.enemyNotifyList[groupIndex] -= OnResourceNotify;
    }

    public void OnResourceNotify()
    {
        Debug.Log(character.unitName + ": Resource notified");
    }
}
