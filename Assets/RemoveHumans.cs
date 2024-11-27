using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveHumans : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Human") && other.GetComponent<Human>().broken)
        {
            other.gameObject.SetActive(false);
        }
    }
}
