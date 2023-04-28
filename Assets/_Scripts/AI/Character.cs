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

    [HideInInspector] public Animator animator;
    public enum AnimalType
    {
        Bear,
        Goat,
        Wolf
    }

    protected virtual void Awake()
    {
        communicationAgent = GetComponent<CommunicationAgent>();

        currentHP = maxHP;
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

    public int maxHP;
    public int currentHP;
    bool dead;
    public virtual void ReceiveDamage(int damage) 
    {
        
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Death();
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    public virtual void Death()
    {
       
      
    }
}
