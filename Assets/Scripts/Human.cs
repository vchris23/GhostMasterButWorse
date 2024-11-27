using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using KadaXuanwu.UtilityDesigner.Scripts;
using KadaXuanwu.UtilityDesigner.Scripts.Evaluation;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Human: MonoBehaviour, ICanTakeDamage
{
    [SerializeField] private ScritableHuman data;
    public new string name;
    [Range(0, 100)]

    [SerializeField] private ConsiderationSet behaviourValues;
    private UtilityDesigner behaviour;
    public bool broken;

    public event Action<Human> terrorChanged;
    public event Action<Human> onBroken;

    public UnityEvent OnBeliefGained = new();
    public UnityEvent OnTerrorGained = new();

    private float Terror
    {
        get => terror;
        set
        {
            value = Mathf.Clamp(value, 0, 100);
            if (Mathf.Approximately(terror, value)) return;
            terrorChanged?.Invoke(this);
            if (value > terror) OnTerrorGained.Invoke();
            terror = value;
            CheckScaredLevel();
        }
    }
    
    public float terror;
    public float belief;
    public Fears conciousFear;
    public Fears unconciousFear;
    
    private Vector3 _destination;
    private NavMeshAgent agent;
    private int scaredLevel;
    [SerializeField] private float calmRate;


    public int numb;

    [SerializeField] private Transform testTrans;

    private void Awake()
    {
        name = data.name;
        terror = data.terror;
        belief = data.belief;
        conciousFear = data.conciousFear;
        unconciousFear = data.unconciousFear;
        agent = GetComponent<NavMeshAgent>();
        behaviour = GetComponent<UtilityDesigner>();
    }
    
    private void FixedUpdate()
    {
        if (!broken)
        {
            Terror -= Time.fixedDeltaTime * calmRate;
        }
    }

    public void TakeDamage(int damage, DamageType type)
    {
        Dictionary<DamageType, float> temp;
        float conciousMod = 1;
        float unconciousMod = 1;
        DamageTable.damageModifiers.TryGetValue(conciousFear, out temp);
        temp?.TryGetValue(type, out conciousMod);
        temp = null;
        DamageTable.damageModifiers.TryGetValue(unconciousFear, out temp);
        temp?.TryGetValue(type, out unconciousMod);
        float modifier = conciousMod *
                         (Mathf.Pow(unconciousMod, 1.3f));
        Terror += damage * modifier * belief;
        print("Took damage: " + (damage * modifier * belief));
    }

    public void GainBelief(float gain)
    {
        if (belief >= 20) return;
        belief += gain;
        print(belief);
        OnBeliefGained.Invoke();
    }

    private void CheckScaredLevel()
    {
        if (broken) return;
        switch (terror)
        {
            case >= 100:
                scaredLevel = 2;
                agent.speed = 5;
                broken = true;
                onBroken?.Invoke(this);
                break;
            case > 80:
                scaredLevel = 2;
                agent.speed = 1;
                break;
            case > 50:
                scaredLevel = 1;
                agent.speed = 2;
                break;
            case < 50:
                scaredLevel = 0;
                agent.speed = 3.5f;
                break;
        }

        behaviourValues.SetConsideration("Terror", terror, behaviour);
    }

    private void MoveTo(Vector3 targetPos)
    {
        if (!Mathf.Approximately(targetPos.x, agent.destination.x) && !Mathf.Approximately(targetPos.z, agent.destination.z))
        {
            agent.SetDestination(targetPos);
        }
    }
    
    
}
