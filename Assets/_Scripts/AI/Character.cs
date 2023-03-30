using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int colonyID = 0;
    public string unitName;
    [Header("Group Management")]
    public GroupManager.GroupPrototype group;

    public GroupManager groupManager;
    [Header("communication Management")]
    public CommunicationAgent communicationAgent;

    protected virtual void Awake()
    {
        communicationAgent = GetComponent<CommunicationAgent>();
    }

    public bool HasGroup()
    {
        if (group == null)
        {
            return false;
        }

        if (group.alliesInGroup == null)
        {
            return false;
        }

        if (group.alliesInGroup.Count == 0)
        {
            return false;
        }

        return true;
      
    }
}
