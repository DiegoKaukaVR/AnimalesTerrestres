using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeScriptableObject : MonoBehaviour
{
    Transform[] transforms;
    [SerializeField] DataScriptableObject dataSO;

    private void Start()
    {
        transforms = GetComponentsInChildren<Transform>();

        for (int i = 0; i < transforms.Length; i++)
        {
            dataSO.pos.Add(transforms[i].position);
        }
        
    }
}
