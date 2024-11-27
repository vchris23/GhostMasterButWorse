using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Ability", fileName = "newAbility", order = 1)]
public class AbilityData : ScriptableObject
{
    public DamageType type;
    public int terrorDamage;
    public float beliefGain;
    public EffectType[] Effect;
    public Target[] Targets;
    public Requirements[] requirements;
    public int plasmaCost;
    public float cooldown;

}
