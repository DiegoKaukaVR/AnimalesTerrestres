using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void EnemyNotify();
public delegate void ResourceNotify();
public class CommunicationManager : MonoBehaviour
{
    public static CommunicationManager instance;
    GroupManager groupManager;

    public event EnemyNotify EnemyNotify;
    public event ResourceNotify ResourceNotify;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        groupManager = GetComponent<GroupManager>();
    }


    public void NotifyEnemyInGroup()
    {
        EnemyNotify.Invoke();
    }

    public void NotifyResourceInGroup()
    {
        ResourceNotify.Invoke();
    }

}
