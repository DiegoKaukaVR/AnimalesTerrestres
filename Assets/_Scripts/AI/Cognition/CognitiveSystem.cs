using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICongitive
{
    public void TickCongition(); 
}

[RequireComponent(typeof (CognitiveManager))]
public class CognitiveSystem : MonoBehaviour, ICongitive
{
    protected CognitiveManager cognitiveManager;

    private void Awake()
    {
        cognitiveManager = GetComponent<CognitiveManager>();
    }
    public virtual void TickCongition() { }
    public bool Detection()
    {
        return false;
    }
}
