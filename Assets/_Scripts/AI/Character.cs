using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int colonyID = 0;
    public string unitName;
    [Header("Group Management")]
    public GroupManager.Group group;
    public GroupManager groupManager;
    [Header("communication Management")]
    public CommunicationAgent communicationAgent;

    private void Awake()
    {
        communicationAgent = GetComponent<CommunicationAgent>();

        group = null;
    }



  

    public bool HasGroup()
    {
        if (group == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
