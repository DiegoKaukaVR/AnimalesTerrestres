using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyMember : MonoBehaviour
{
    public Family family;


    public Character entity;

    public Character father;
    public Character mother;

    /// Sons of your fathers/mothers
    public List<Character> brothers;

    /// Sons of any of your parents with other families
    public List<Character> stepBrothers;

    /// Cousins, uncles, aunts, grandfathers, grandmothers...
    public List<Character> relatives;


    private void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        entity = GetComponent<Character>();
    }

    private void OnValidate()
    {
        Initialize();
    }
}


