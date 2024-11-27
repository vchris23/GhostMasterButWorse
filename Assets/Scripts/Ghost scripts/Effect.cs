using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

[Serializable]
public class Effect: MonoBehaviour
{
    public static DamageType type;

    public static Action<object, Ability> GetEffectAction(EffectType effect)
    {
        switch (effect)
        {
            case EffectType.Damage:
                return Damage;
            case EffectType.Freeze:
                return null;
            case EffectType.Swirley:
                return Swirley;
            default:
                throw new ArgumentOutOfRangeException(nameof(effect), effect, null);
        }
    }
    
    static void Damage(object target, Ability ability)
    {
        if (target.GetType().GetInterfaces().Contains(typeof(ICanTakeDamage)))
        {
            (target as ICanTakeDamage)?.TakeDamage(ability.TerrorDamage, ability.Type);
        }
        if (target.GetType() == typeof(Human))
        { 
            Human human = target as Human;
            human?.GainBelief(ability.BeliefGain);
        }

        if (target.GetType() == typeof(Human[]))
        {
            Human[] humans = target as Human[];
            
            if (humans != null)
            {
                foreach (Human varHuman in humans)
                {
                    Damage(varHuman, ability);
                }
            }
        }
    }

    static void Swirley(object target, Ability ability)
    {
        if (target.GetType() == typeof(GameObject[]))
        {
            foreach (GameObject entity in ((GameObject[])target))
            {
                Rigidbody entityRig;
                if (entity.TryGetComponent(out entityRig))
                {
                    Vector3 force = new(Random.Range(-1f, 1f), 0.1f, Random.Range(-1f, 1f));
                    entityRig.AddForce(force*15, ForceMode.Impulse);
                }
            }
        }
        if (target is GameObject obj)
        {
            Rigidbody entityRig = obj.GetComponent<Rigidbody>();
            Vector3 force = new(Random.Range(-1f, 1f), 0.1f, Random.Range(-1f, 1f)); 
            entityRig.AddForce(force*15, ForceMode.Impulse);
        }
    }
    
}

public enum EffectType
{
    Damage,
    Swirley,
    Freeze
}