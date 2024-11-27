using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KadaXuanwu.UtilityDesigner.Scripts.Execution.Runtime;
using UnityEngine;

public class EntityRefManager : SceneReferences
{
    [SerializeField] public List<UtilityEntity> entities;
    [SerializeField] private List<Transform> presetTransforms;
    protected override void RegisterCustomLists()
    {
        entities = FindObjectsOfType<UtilityEntity>().ToList();
        AddList(entities);
        AddList(presetTransforms);
    }
}
