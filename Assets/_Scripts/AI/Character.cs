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

    public List<Transform> EnemyTransforms;
    public enum AnimalType
    {
        Bear,
        Goat,
        Wolf
    }

    bool alert = false;
    public bool dead;
    public bool IsAlert()
    {
        if (alert)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SendAlertMessage()
    {
        communicationAgent.SendAlertMessageGroup();
    }
    public void SendEnemyTransforms(List<Transform> targets)
    {
        communicationAgent.SendEnemiesTargets(targets);
    }
    public void ReceiveAlertMessage()
    {
        Debug.Log("Unit: " + unitName + "ha sido alertado de un enemigo!!");
        alert = true;
    }
    public void ReceiveEnemyTransfroms(List<Transform> targets)
    {
        EnemyTransforms = targets;
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
        dead = true;

    }
}
