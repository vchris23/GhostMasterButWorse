using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class endScreenScript : MonoBehaviour
{

    [SerializeField] private TMP_Text time_elaped;
    
    public void revealScreen(float timer)
    {
        gameObject.SetActive(true);
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        time_elaped.text = $"{minutes}:{seconds}";

    }
    
}
