using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Ghost", menuName = "Scriptable Objects/Ghost")]
public class GhostData : ScriptableObject
{
    public new string name;
    public Fetter[] fetters;
    public GameObject prefab;
    [SerializeField] private AbilityData[] abilityDatas;
    [HideInInspector] public Ability[] abilities;

    private void OnValidate()
    {
        abilities = new Ability[abilityDatas.Length];
        for (int i = 0; i < abilityDatas.Length; i++)
        {
            abilities[i] = new Ability(abilityDatas[i]);
        }

        abilities = abilities.OrderBy(ability => ability.PlasmaCost).ToArray();
    }
}
