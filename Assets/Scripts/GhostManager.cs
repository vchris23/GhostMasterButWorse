using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GhostManager : MonoBehaviour
{
    public static int plasmaUse;
    private static int _maxPlasma;

    public static int maxPlasma
    {
        get => _maxPlasma;
        set
        {
            _maxPlasma = value + 100;
            OnPlasmaRecalculated?.Invoke();
        }
    }
    [SerializeField] private GhostData[] ghostsData;
    private Ghost[] ghosts;
    [SerializeField] private Transform ghostPanel;
    [SerializeField] private GhostPanel panel;
    public static event Action OnPlasmaRecalculated;
    

    private void Awake()
    {
        maxPlasma = 0;
        ghosts = new Ghost[ghostsData.Length];
        for (int i = 0; i < ghostsData.Length; i++)
        {
            GhostData data = ghostsData[i];
            GameObject ghostObject = Instantiate(data.prefab);
            ghostObject.SetActive(false);
            ghosts[i] = ghostObject.GetComponent<Ghost>();
            ghosts[i].CostChanged += UpdatePlasma;
        }
        panel.PopulatePanel(ghosts);
        UpdatePlasma(ghosts[0]);
    }

    private void UpdatePlasma(Ghost updatedGhost)
    {
        plasmaUse = 0;
        foreach (Ghost ghost in ghosts)
        {
            plasmaUse += ghost.isActiveAndEnabled ? ghost.getAbilityCost() : 0;
        }

        print(plasmaUse);
        print("Updated plasma");
        OnPlasmaRecalculated?.Invoke();
    }
    
}