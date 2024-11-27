using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

[Serializable]
public class Ability
{
    public string name;
    public readonly DamageType Type;
    public readonly int TerrorDamage;
    public readonly float BeliefGain;
    private Action<object, Ability>[] _effects;
    private Target[] _targets;
    public Requirements[] requirements;
    public readonly int PlasmaCost;
    public readonly float Cooldown;
    public float CurrentCooldown { get; private set; }
    public bool CanBeUsed { get; private set; }

    public Func<string, string, Task<bool>> animationMethod;
    private Task<bool> animationTask;

    public Ability(AbilityData data)
    {
        name = data.name;
        Type = data.type;
        TerrorDamage = data.terrorDamage;
        BeliefGain = data.beliefGain;
        
        _effects = new Action<object, Ability>[data.Effect.Length];
        for (int i = 0; i < data.Effect.Length; i++)
        {
            _effects[i] = Effect.GetEffectAction(data.Effect[i]);
        }

        _targets = data.Targets;
        requirements = data.requirements;
        PlasmaCost = data.plasmaCost;
        Cooldown = data.cooldown;
        CurrentCooldown = 0;
        CanBeUsed = true;
    }

    public async Task<bool> Cast(Room currentRoom) //Casts this ability, returns true if the cast was succesful, if not returns false
    {
        if (!CanBeUsed || animationTask != null) return false;
        object[] targets = new object[_effects.Length];
        for (int i = 0; i < _effects.Length; i++)
        {
            targets[i] = Targeter.FindTarget(_targets[i], currentRoom);
            
            if (targets[i] == null)
            {
                return false;
            }

            if (targets[i].GetType().IsArray && ((Array)targets[i]).Length == 0)
            {
                return false;
            }
        }
        animationTask = animationMethod.Invoke("Cast", "Casting");
        await animationTask;
        if (animationTask.Result == false)
        {
            animationTask.Dispose();
            animationTask = null;
            return false;
        }

        animationTask.Dispose();
        animationTask = null;
        for (int i = 0; i < _effects.Length; i++)
        {
            _effects[i](targets[i], this);
        }
        BeginCooldown();
        return true;
            
    }

    public void BeginCooldown()
    {
        CanBeUsed = false;
        CurrentCooldown = Cooldown;
        StartCountDown();
    }
    
    private async void StartCountDown()
    {
        while (CurrentCooldown > 0)
        {
            CurrentCooldown -= Time.deltaTime;
            await Task.Yield();
        }   
        CanBeUsed = true;
    }
}