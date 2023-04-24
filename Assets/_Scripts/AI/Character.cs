using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public AnimalType specie;
    public string unitName;

    [Header("Group Management")]
    public GroupManager.GroupPrototype group;

    public GroupManager groupManager;
    [Header("communication Management")]
    public CommunicationAgent communicationAgent;

   
    public enum AnimalType
    {
        Bear,
        Goat,
        Wolf
    }

    protected virtual void Awake()
    {
        communicationAgent = GetComponent<CommunicationAgent>();
      
    }


    protected virtual void Start()
    {
        UnitManager.instance.AddCharacter(this);
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
