using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DataScriptableObject", menuName = "ScriptableObjects/DataScriptableObject", order = 1)]
public class DataScriptableObject : ScriptableObject
{
    public List<Vector3> pos = new List<Vector3>();
}
