using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HumanPanelController : MonoBehaviour
{
    private Image[] humanPanels;
    [SerializeField] private GameObject humanPopup;
    public static  Human selectedHuman;
    
    
    private void Awake()
    {
        humanPanels = new Image[HumanManager.humans.Count];
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (i <= HumanManager.humans.Count - 1)
            {
                child.gameObject.SetActive(true);
                humanPanels[i] = child.GetComponent<Image>();
                child.GetComponent<OnHoverHuman>().Initialize(humanPopup, HumanManager.humans[i]);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
            
        }
    }
}
