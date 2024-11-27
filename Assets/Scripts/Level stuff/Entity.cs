using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Entity : MonoBehaviour, ICanTakeDamage
{
    [SerializeField] private int health;
    public Fetter fetter;
    [SerializeField] private DamageType[] damagedBy;
    public Ghost currentConnectedGhost;
    [SerializeField] private GameObject highlightObject;
    
    public void TakeDamage(int damage, DamageType type)
    {
        if (damagedBy.Contains(type))
        {
            health -= damage;
        }
    }

    private void OnMouseEnter()
    {
        Highlight();
        MouseSelector.CurrentSelectedFetter = this;
    }
    
    private void OnMouseExit()
    {
        UnHighlight();
        MouseSelector.CurrentSelectedFetter = null;
    }

    public void Highlight()
    {
        if (fetter == Fetter.None) return;
        highlightObject.SetActive(true);
    }
    public void UnHighlight()
    {
        highlightObject.SetActive(false);
    }
}
