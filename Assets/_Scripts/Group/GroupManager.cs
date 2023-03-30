using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour
{
    public enum GroupOrganization
    {
        ByDistance,
        ByFamily,
        ByPower
    }
    public GroupOrganization currentOrganization;

    public Character[] allCharacters;

    [Header("Group Manager")]
    public List<GroupPrototype> groupList = new List<GroupPrototype>();


    [System.Serializable]
    public class GroupPrototype
    {
        public GroupPrototype(Character char1, Character char2)
        {
            alliesInGroup = new List<Character>() { char1, char2 };
        }

        public List<Character> alliesInGroup;

    }

    public int GetGroupIndex(GroupPrototype group)
    {
        int index = 0;

        for (int i = 0; i < groupList.Count; i++)
        {
            if (group == groupList[i])
            {
                index = i;
            }
        }

        return index;
    }


    public float minDistToCenter = 5f;
    public float minDistToGroup = 5f;

    private void Awake()
    {
        for (int i = 0; i < allCharacters.Length; i++)
        {
            allCharacters[i].group = null;
        }
    }

    private void Update()
    {
        if (Time.frameCount % 5 == 0)
        {
            ManageGroups();
        }
    }

    public void ManageGroups()
    {
        switch (currentOrganization)
        {
            case GroupOrganization.ByDistance:
                GroupAlgorithmByDistance();
                break;
            case GroupOrganization.ByFamily:
                GroupAlgorithmByFamily();
                break;
            case GroupOrganization.ByPower:
                GroupAlgorithmByPower();
                break;
            default:
                break;
        }
    }
    float dist;
    void GroupAlgorithmByDistance()
    {
        for (int i = 0; i < allCharacters.Length; i++)
        {
            for (int y = 0; y < allCharacters.Length; y++)
            {
                if (i == y)
                {
                    continue;
                }
                
                dist = Vector3.Distance(allCharacters[i].transform.position, allCharacters[y].transform.position);
                
                if (dist < minDistToGroup)
                {
                    ManageGroup(allCharacters[i], allCharacters[y]);
                }
                else
                {
                    if (allCharacters[i].group != null && allCharacters[i].group == allCharacters[y].group)
                    {
                        RemoveCharacterFromGroup(allCharacters[i], allCharacters[i].group);
                        RemoveCharacterFromGroup(allCharacters[y], allCharacters[y].group);
                    }
                }

            }
        }
    }
    void GroupAlgorithmByFamily()
    {

    }
    void GroupAlgorithmByPower()
    {

    }



    void ManageGroup(Character a, Character b)
    {
        if (a.HasGroup() && b.HasGroup() && a.group == b.group)
        {
            return;
        }
        if (a.HasGroup() && b.HasGroup())
        {
            // Combine
            CombineGroups(a.group, b.group);
        }
        if (a.HasGroup() && !b.HasGroup())
        {
            // Add b to A.group
            AddCharacterToGroup(b, a.group);
        }
        if (!a.HasGroup() && b.HasGroup())
        {
            // Add a to B.group
            AddCharacterToGroup(a, b.group);
        }

        if (!a.HasGroup() && !b.HasGroup())
        {
            CreateGroup(a, b);
        }
    }

    void AddCharacterToGroup(Character a, GroupPrototype group)
    {
        a.group = group;
        group.alliesInGroup.Add(a);
    }
    void RemoveCharacterFromGroup(Character a, GroupPrototype group)
    {
        for (int i = 0; i < group.alliesInGroup.Count; i++)
        {
            if (group.alliesInGroup[i] == a)
            {
                continue;
            }

            dist = Vector3.Distance(a.transform.position, group.alliesInGroup[i].transform.position);

            if (dist<minDistToGroup)
            {
                return;
            }
        }

        group.alliesInGroup.Remove(a);
        a.group = null;

        if (group.alliesInGroup.Count == 0)
        {
            for (int i = 0; i < groupList.Count; i++)
            {
                if (groupList[i] == group)
                {
                    groupList.Remove(group);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Add B to A
    /// </summary>
    void CombineGroups(GroupPrototype A, GroupPrototype B)
    {
        if (A == B)
        {
            return;
        }
        A.alliesInGroup.AddRange(B.alliesInGroup);

        for (int i = 0; i < B.alliesInGroup.Count; i++)
        {
            B.alliesInGroup[i].group = A;
        }
    }

    void CreateGroup(Character a, Character b)
    {
        GroupPrototype newGroup = new GroupPrototype(a, b);
        a.group = newGroup;
        b.group = newGroup;
        groupList.Add(newGroup);
    }


    public Vector3 CalculateCenter(int indexGroup)
    {
        Vector3 pos = Vector3.zero;
        int populationGroup = 0;

        if (groupList[indexGroup].alliesInGroup.Count == 0)
        {
            return Vector3.zero;
        }

        for (int i = 0; i < groupList[indexGroup].alliesInGroup.Count; i++)
        {
            pos += groupList[indexGroup].alliesInGroup[i].transform.position;
            populationGroup++;
        }

        return pos / populationGroup;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (groupList.Count == 0)
        {
            return;
        }

        switch (currentOrganization)
        {
            case GroupOrganization.ByDistance:

                for (int i = 0; i < groupList.Count; i++)
                {
                    Vector3 center = CalculateCenter(i);

                    if (center == Vector3.zero)
                    {
                        continue;
                    }

                    Gizmos.DrawCube(center, Vector3.one * 0.3f);
                    Gizmos.DrawWireSphere(center, minDistToCenter);
                }

                break;
            case GroupOrganization.ByFamily:
                break;
            case GroupOrganization.ByPower:
                break;
            default:
                break;
        }

       

        





    }
}
