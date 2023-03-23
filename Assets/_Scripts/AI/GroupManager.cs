using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupManager : MonoBehaviour
{
    public List<Character> allCharacters = new List<Character>();
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
    }


    int numberOfColony = 0;
    public float minDistToCenter = 5f;
    public float minDistToGroup = 5f;

    private void Update()
    {
        CheckAllGroups();
    }

    void CheckAllGroups()
    {
        for (int i = 0; i < allCharacters.Count; i++)
        {
            Vector3 center = CalculateCenter();

            if (center == Vector3.zero)
            {
                center = allCharacters[i].transform.position;
                AddAllyToGroup(allCharacters[i]);
                continue;
            }
            float dist = Vector3.Distance(allCharacters[i].transform.position, center);
            if (dist < minDistToCenter)
            {
                AddAllyToGroup(allCharacters[i]);
            }
            else
            {
                RemoveAllyFromGroup(allCharacters[i]);
            }
        }


        #region ExtensiveMethod

        //for (int i = 0; i < allCharacters.Count; i++)
        //{
        //    for (int y = 0; y < allCharacters.Count; y++)
        //    {
        //        if (i == y)
        //        {
        //            continue;
        //        }

        //        float dist = Vector3.Distance(allCharacters[i].transform.position, allCharacters[y].transform.position);
        //        if (dist<minDistToGroup)
        //        {
        //            Group newGroup = new Group(allCharacters[i], allCharacters[y]);
        //            groupList.Add(newGroup);
        //        }




        //    }
        //}

        #endregion
        #region IntesiveMethod
        //for (int i = 0; i < groupList.Count; i++)
        //{
        //    /// Calculate center of all group members
        //    groupList[i].center = CalculateCenter();

        //    for (int y = 0; y < allCharacters.Count; y++)
        //    {
        //        /// If unit is already in other group
        //        if (allCharacters[i].group != null && allCharacters[i].group != groupList[i])
        //        {
        //            continue;
        //        }


        //        float dist = Vector3.Distance(allCharacters[i].transform.position, groupList[i].center);

        //        if (dist < minDistToCenter)
        //        {
        //            AddAllyToGroup(allCharacters[i], i);
        //        }
        //        else
        //        {
        //            RemoveAllyFromGroup(allCharacters[i], i);
        //        }
        //    }

        //}
        #endregion

    }
    public Vector3 CalculateCenter()
    {
        Vector3 pos = Vector3.zero;
        int populationGroup = 0;

        if (alliesInGroup.Count == 0)
        {
            return Vector3.zero;
        }

        for (int i = 0; i < alliesInGroup.Count; i++)
        {
            pos += alliesInGroup[i].transform.position;
            populationGroup++;
        }

        return pos/populationGroup;
    }

    public void AddAllyToGroup(Character ally)
    {
        if (alliesInGroup.Contains(ally))
        {
            return;
        }

        ally.communicationAgent.RegisterEvents();
        alliesInGroup.Add(ally);

    }
    public void RemoveAllyFromGroup(Character ally)
    {
        if (!alliesInGroup.Contains(ally))
        {
            return;
        }
        alliesInGroup.Remove(ally);
        ally.group = null;
    }
    public void AddAllyToGroup(Character ally, int groupIndex)
    {
        if (groupList[groupIndex].alliesInGroup.Contains(ally))
        {
            return;
        }
        groupList[groupIndex].alliesInGroup.Add(ally);
    }
    public void RemoveAllyFromGroup(Character ally, int groupIndex)
    {
        if (!groupList[groupIndex].alliesInGroup.Contains(ally))
        {
            return;
        }
        groupList[groupIndex].alliesInGroup.Remove(ally);

        ally.communicationAgent.RegisterEvents();
        ally.group = null;
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (alliesInGroup.Count == 0)
        {
            return;
        }
       
        Vector3 center = CalculateCenter();

        if (center == Vector3.zero)
        {
            return;
        }

        Gizmos.DrawCube(center, Vector3.one * 0.3f);
        Gizmos.DrawWireSphere(center, minDistToCenter);

        for (int i = 0; i < alliesInGroup.Count; i++)
        {
            if (Vector3.Distance(alliesInGroup[i].transform.position, center) > minDistToCenter)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(alliesInGroup[i].transform.position, center);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(alliesInGroup[i].transform.position, center);
            }
        }
            
        
       
      
     
    }
}
