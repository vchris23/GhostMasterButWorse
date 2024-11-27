using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Room : MonoBehaviour
{
    public List<Entity> entities;
    public List<Human> humansInRoom;
    public List<GameObject> clutter;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.TryGetComponent<Human>(out Human enteredHuman)) //If what has just hit the collider is a human, add them to the list
        {
            humansInRoom.Add(enteredHuman);
        }

        if (other.CompareTag("Entity"))
        {
            entities.Add(other.GetComponent<Entity>());
        }
        
        if (other.CompareTag("Clutter"))
        {
            clutter.Add(other.gameObject);
        }
        
        if (other.TryGetComponent(out Ghost enteredGhost)) //If a ghost entered, then this room is set as the one they are in
        {
            enteredGhost.CurrentRoom = this;
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Human enteredHuman)) //If what has just hit the collider is a human, remove them from the list
        {
            humansInRoom.Remove(enteredHuman);
        }
        
        if (other.CompareTag("Entity"))
        {
            entities.Remove(other.GetComponent<Entity>());
        }
        
        if (other.CompareTag("Clutter"))
        {
            clutter.Remove(other.gameObject);
        }
    }
}
