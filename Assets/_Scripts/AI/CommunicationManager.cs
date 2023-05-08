using System.Collections.Generic;
using UnityEngine;
using System; 

public delegate void EnemyNotify();
public delegate void EnemyPositions(Transform transforms);
public delegate void ResourceNotify();

public delegate void EventHandler(object? sender, EventArgs e);

public class CommunicationManager : MonoBehaviour
{
    public static CommunicationManager instance;
    GroupManager groupManager;

    public event EnemyNotify OnEnemyNotify;

    public EnemyNotify[] enemyNotifyList = new EnemyNotify[10];
    public EnemyPositions[] enemyNotifyPositions = new EnemyPositions[10];
    public event ResourceNotify ResourceNotify;

    public event EventHandler a;

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


    public void NotifyEnemyInGroup(int index)
    {
        /// TESTEO
        //enemyNotifyList[0].Invoke();
        //enemyNotifyList[1].Invoke();

        /// FUNCIONAL
        ///   //if (EnemyNotify != null)
        //{
        //    EnemyNotify.Invoke();
        //}


        if (enemyNotifyList[index] != null)
        {
            enemyNotifyList[index].Invoke();
        } 

      
      
    }

    public void NotifyResourceInGroup()
    {
        if (ResourceNotify != null)
        {
            ResourceNotify.Invoke();
        }
       
    }

}
