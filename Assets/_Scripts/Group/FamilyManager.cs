using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Family
{
    public string familyName;
    public List<FamilyMember> familyMembers;
}

public class FamilyManager : MonoBehaviour
{

    public List<Family> AllFamilies = new List<Family>();

    private void Awake()
    {
        for (int i = 0; i < AllFamilies.Count; i++)
        {
            for (int j = 0; j < AllFamilies[i].familyMembers.Count; j++)
            {
                AllFamilies[i].familyMembers[j].family = AllFamilies[i];
            }
        }
    }

}
