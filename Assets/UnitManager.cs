using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;

    public List<Character> allUnits;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddCharacter(Character newUnit)
    {
        if (allUnits.Contains(newUnit))
        {
            return;
        }

        allUnits.Add(newUnit);
    }

    public Character[] TargetAnimals(Character.AnimalType targetSpecie)
    {
        List<Character> targetAnimals = new List<Character>();

        for (int i = 0; i < allUnits.Count; i++)
        {
            if (allUnits[i].specie == targetSpecie)
            {
                targetAnimals.Add(allUnits[i]);
            }
        }

        return targetAnimals.ToArray();
    }
}
