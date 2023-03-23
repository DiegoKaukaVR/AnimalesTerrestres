using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int colonyID = 0;
    public string unitName;
    public GroupManager.Group group;
    public CommunicationAgent communicationAgent;

    private void Awake()
    {
        communicationAgent = GetComponent<CommunicationAgent>();
    }
}
