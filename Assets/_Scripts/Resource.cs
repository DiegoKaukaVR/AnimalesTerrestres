using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool isAvaible()
    {
        if (avaible)
        {
            avaible = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool avaible = true;
}
