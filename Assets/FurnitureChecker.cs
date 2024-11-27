using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KadaXuanwu.UtilityDesigner.Scripts;
using KadaXuanwu.UtilityDesigner.Scripts.Evaluation;
using UnityEngine;
using UnityEngine.InputSystem;

public class FurnitureChecker : MonoBehaviour
{
    private UtilityDesigner designer;

    [SerializeField] private ConsiderationSet considerations;

    [SerializeField] private EntityRefManager entityManager;

    void Awake()
    {
        designer = GetComponent<UtilityDesigner>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (EntityType type in Enum.GetValues(typeof(EntityType)))
        {
            int count = entityManager.entities.Count(x => x.type == type && (!x.occupiedBy || x.occupiedBy == gameObject));
            //print($"For object {gameObject.name} and designer {designer.gameObject.name}, count for type: {type}: {count}");
            considerations.SetConsideration(type.ToString(), count, designer);
            //print("Getting consideration: " + considerations.GetConsideration(type.ToString()));
        }
    }
}
