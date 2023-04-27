using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CommunicationAction();

/// <summary>
/// Important to Unregister Event when Destroy object Ondisable
/// </summary>
public class SimpleCommunication : MonoBehaviour
{

    public event CommunicationAction OnCommunicationEvent;

    public void SendMessage()
    {
        OnCommunicationEvent?.Invoke();
    }
    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendMessage();
        }
    }

}
