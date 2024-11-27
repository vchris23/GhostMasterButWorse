using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanManager : MonoBehaviour
{

    public static List<Human> humans = new();
    public int brokenHumans = 0;
    public static int combinedTerror = 0;
    public event Action onAllHumansDead;

    private void Awake()
    {
        humans = FindObjectsOfType<Human>().ToList();
        
        foreach (Human human in humans)
        {
            human.terrorChanged += UpdateCombinedTerror;
            human.onBroken += reactToBrokenHuman;
        }
    }
    
    private void UpdateCombinedTerror(Human changedHuman)
    {
        combinedTerror = 0;
        foreach (Human human in humans)
        {
            combinedTerror += (int) human.terror;
        }

        GhostManager.maxPlasma = combinedTerror;
    }

    private void reactToBrokenHuman(Human brokenHuman)
    {
        brokenHumans++;
        if (brokenHumans == humans.Count)
        {
            onAllHumansDead?.Invoke();
        }
    }
}
