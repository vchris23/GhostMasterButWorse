using System.Collections;
using System.Collections.Generic;
using KadaXuanwu.UtilityDesigner.Scripts.Execution.Actions;
using KadaXuanwu.UtilityDesigner.Scripts.Execution.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : ActionNode
{
    private Humanblackboard blackboard;
    private NavMeshAgent agent;

    protected override void OnAwake()
    {
        blackboard = ThisGameObject.GetComponent<Humanblackboard>();
        agent = ThisGameObject.GetComponent<NavMeshAgent>();
    }

    protected override NodeState OnUpdate()
    {
        
        agent.destination = blackboard.target.interactionSpot? blackboard.target.interactionSpot.position: blackboard.target.transform.position;
        
        float distance = Vector3.Distance(ThisGameObject.transform.position, agent.destination);

        if (distance > 1.5)
        {
            return NodeState.Running;
        }
            
        return NodeState.Success;
        
    }
}
