using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KadaXuanwu.UtilityDesigner.Scripts.Execution.Runtime;
using UnityEngine;
using Random = System.Random;

public class FindEntityOfType : ActionNode
{

    public EntityType typeToFind;

    protected override NodeState OnUpdate()
    {
        UtilityEntity[] relevantEntities = SceneRefs.GetListOfType<UtilityEntity>().Where(x => x.type == typeToFind && (!x.occupiedBy || x.occupiedBy == ThisGameObject)).ToArray();
        if (relevantEntities.Length == 0)
        {
            Debug.Log("No entities found for " + ThisGameObject.name);
            return NodeState.Failure;
        }
        UtilityEntity relevantEntity = relevantEntities.OrderBy(x => new Random().Next()).First();
        relevantEntity.occupiedBy = ThisGameObject;
        ThisGameObject.GetComponent<Humanblackboard>().target = relevantEntity;
        Debug.Log("Entity found for " + ThisGameObject.name);
        return NodeState.Success;
    }
    
}
