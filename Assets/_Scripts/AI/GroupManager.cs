using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour
{
    public Character[] allCharacters;
    public List<Character> alliesInGroup = new List<Character>();

    [Header("Group Manager")]
    public List<Group> groupList = new List<Group>();

  
    [System.Serializable]
    public class Group
    {
        public Group(Character char1, Character char2)
        {
            alliesInGroup = new List<Character>() { char1, char2 };
        }
        public List<Character> alliesInGroup;
        public Vector3 center;
        public int idColony;
        public int indexGroup;
    }

    public int GetGroupIndex(Group group)
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
            GroupAlgorithm();
        }
    }
    float dist;
    void GroupAlgorithm()
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

   
    void ManageGroup(Character a, Character b)
    {
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

    void AddCharacterToGroup(Character a, Group group)
    {
        if (group == null)
        {
            return;
        }

        a.group = group;
        group.alliesInGroup.Add(a);
    }
    void RemoveCharacterFromGroup(Character a, Group group)
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
    void CombineGroups(Group A, Group B)
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
        Group newGroup = new Group(a, b);
        a.group = newGroup;
        b.group = newGroup;
        groupList.Add(newGroup);
    }















    //void CheckAllGroups()
    //{
    //    for (int i = 0; i < allCharacters.Length; i++)
    //    {
    //        Vector3 center = CalculateCenter();

    //        if (center == Vector3.zero)
    //        {
    //            center = allCharacters[i].transform.position;
    //            AddAllyToGroup(allCharacters[i]);
    //            continue;
    //        }
    //        float dist = Vector3.Distance(allCharacters[i].transform.position, center);
    //        if (dist < minDistToCenter)
    //        {
    //            AddAllyToGroup(allCharacters[i]);
    //        }
    //        else
    //        {
    //            RemoveAllyFromGroup(allCharacters[i]);
    //        }
    //    }


    //    #region ExtensiveMethod

    //    //for (int i = 0; i < allCharacters.Count; i++)
    //    //{
    //    //    for (int y = 0; y < allCharacters.Count; y++)
    //    //    {
    //    //        if (i == y)
    //    //        {
    //    //            continue;
    //    //        }

    //    //        float dist = Vector3.Distance(allCharacters[i].transform.position, allCharacters[y].transform.position);
    //    //        if (dist<minDistToGroup)
    //    //        {
    //    //            Group newGroup = new Group(allCharacters[i], allCharacters[y]);
    //    //            groupList.Add(newGroup);
    //    //        }




    //    //    }
    //    //}

    //    #endregion
    //    #region IntesiveMethod
    //    //for (int i = 0; i < groupList.Count; i++)
    //    //{
    //    //    /// Calculate center of all group members
    //    //    groupList[i].center = CalculateCenter();

    //    //    for (int y = 0; y < allCharacters.Count; y++)
    //    //    {
    //    //        /// If unit is already in other group
    //    //        if (allCharacters[i].group != null && allCharacters[i].group != groupList[i])
    //    //        {
    //    //            continue;
    //    //        }


    //    //        float dist = Vector3.Distance(allCharacters[i].transform.position, groupList[i].center);

    //    //        if (dist < minDistToCenter)
    //    //        {
    //    //            AddAllyToGroup(allCharacters[i], i);
    //    //        }
    //    //        else
    //    //        {
    //    //            RemoveAllyFromGroup(allCharacters[i], i);
    //    //        }
    //    //    }

    //    //}
    //    #endregion

    //}
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

    //public void AddAllyToGroup(Character ally)
    //{
    //    if (alliesInGroup.Contains(ally))
    //    {
    //        return;
    //    }

    //    ally.communicationAgent.RegisterEvents();
    //    alliesInGroup.Add(ally);

    //}
    //public void RemoveAllyFromGroup(Character ally)
    //{
    //    if (!alliesInGroup.Contains(ally))
    //    {
    //        return;
    //    }
    //    alliesInGroup.Remove(ally);
    //    ally.group = null;
    //}
    //public void AddAllyToGroup(Character ally, int groupIndex)
    //{
    //    if (groupList[groupIndex].alliesInGroup.Contains(ally))
    //    {
    //        return;
    //    }
    //    groupList[groupIndex].alliesInGroup.Add(ally);
    //}
    //public void RemoveAllyFromGroup(Character ally, int groupIndex)
    //{
    //    if (!groupList[groupIndex].alliesInGroup.Contains(ally))
    //    {
    //        return;
    //    }
    //    groupList[groupIndex].alliesInGroup.Remove(ally);

    //    ally.communicationAgent.RegisterEvents();
    //    ally.group = null;
    //}


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (groupList.Count == 0)
        {
            return;
        }

        for (int i = 0; i <groupList.Count; i++)
        {
            Vector3 center = CalculateCenter(i);

            if (center == Vector3.zero)
            {
                continue;
            }

            Gizmos.DrawCube(center, Vector3.one * 0.3f);
            Gizmos.DrawWireSphere(center, minDistToCenter);

            //for (int y = 0; y < groupList[i].alliesInGroup.Count; y++)
            //{
            //    if (Vector3.Distance(groupList[i].alliesInGroup[y].transform.position, center) > minDistToCenter)
            //    {
            //        Gizmos.color = Color.red;
            //        Gizmos.DrawLine(groupList[i].alliesInGroup[y].transform.position, center);
            //    }
            //    else
            //    {
            //        Gizmos.color = Color.green;
            //        Gizmos.DrawLine(groupList[i].alliesInGroup[y].transform.position, center);
            //    }
            //}
        }

        





    }
}
