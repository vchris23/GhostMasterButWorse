using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "DamageTable", menuName = "Scriptable Objects/Damage Table", order = 1)]
public class DamageTable : ScriptableObject
{

    public static Dictionary<Fears, Dictionary<DamageType, float>> damageModifiers = new();
    [SerializeField] public DamageCombo[] multipliers;
    public void OnValidate()
    {
        damageModifiers = new();
        foreach (Fears fear in Enum.GetValues(typeof(Fears)))
        {
            damageModifiers[fear] = new Dictionary<DamageType, float>();
            
            foreach (DamageType damageType in Enum.GetValues(typeof(DamageType)))
            {
                damageModifiers[fear][damageType] = 1f;
            }
        }
        foreach (DamageCombo mult in multipliers)
        {
            damageModifiers[mult.fear][mult.damage] = mult.multiplier;
        }
    }
}
