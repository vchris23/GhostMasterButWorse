using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class Ghost : MonoBehaviour
{
    [SerializeField] public GhostData data;
    private Animator _animator;
    public GameObject prefab;
    public Fetter[] Fetters { get; private set; }
    public Entity currentFetter;
    public Ability[] _abilities;
    public Task abilityCheck;

    public event Action<Ghost> CostChanged;

    private int _abilitySelected = 0;

    public int AbilitySelected
    {
        get => _abilitySelected;
        set
        {
            if (value == _abilitySelected) return;
            _abilitySelected = value;
            CostChanged?.Invoke(this);
        }
    }
    
    
    public Room CurrentRoom { private get; set; }

    private void Awake()
    {
        Fetters = data.fetters;
        _abilities = data.abilities;
        name = data.name;
        _animator = GetComponent<Animator>();

        foreach (Ability ability in _abilities)
        {
            ability.animationMethod = TriggerAnimation;
        }

        abilityCheck = CheckAndPlayAbilities();

    }

    private async Task CheckAndPlayAbilities()
    {
        if (AbilitySelected == 0)
        {
            return;
        }
        for (int i = AbilitySelected - 1; i >= 0; i--) //Casts abilities up to and including the one that has currently been selected
        {
            await _abilities[i].Cast(CurrentRoom);
        }
    }
    
    private void FixedUpdate()
    {
        if (abilityCheck.Status != TaskStatus.Running)
        {
            //abilityCheck.Dispose();
            abilityCheck = CheckAndPlayAbilities();
        }
    }


    public bool changeEntity(Entity newEntity)
    {
        if (!data.fetters.Contains(newEntity.fetter) || newEntity.currentConnectedGhost) return false; //Returns false if ghost is unable to change position to that entity
        
        if (currentFetter) currentFetter.currentConnectedGhost = null; //If we have been on an entity before this, we have to label it as empty, so a ghost can spawn there again
        
        gameObject.SetActive(true);
        currentFetter = newEntity;
        currentFetter.currentConnectedGhost = this;
        transform.position = newEntity.transform.position + new Vector3(0, 2, 0);
        return true;
    }

    public void bench()
    {
        CostChanged?.Invoke(this);
        currentFetter.currentConnectedGhost = null;
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        CostChanged?.Invoke(this);
    }

    public int getAbilityCost()
    {
        if (_abilitySelected == 0) return 0;
        
        int cost = 0;
        for (int i = 0; i < AbilitySelected; i++)
        {
            cost += _abilities[i].PlasmaCost;
        }
        
        return cost;
    }

    private async Task<bool> TriggerAnimation(string triggerName, string stateName)
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(stateName)) return false;
        _animator.SetTrigger(triggerName);
        bool hasStarted = false;
        bool isPlaying = _animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        
        await Task.Delay(20);
        while ((_animator.IsInTransition(0) && !hasStarted) || isPlaying)
        {
            await Task.Yield();
            isPlaying = _animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
            if (!hasStarted && isPlaying) hasStarted = true;
            if (isPlaying && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                return true;
            }

        }
        return false;
    }
    
}
