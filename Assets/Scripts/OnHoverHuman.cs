using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class OnHoverHuman : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject popup;
    private Human thisHuman;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        HumanPanelController.selectedHuman = thisHuman;
        popup.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (HumanPanelController.selectedHuman == thisHuman)
        {
            HumanPanelController.selectedHuman = null;
            popup.SetActive(false);
        }
    }
    
    public void Initialize(GameObject popup, Human human)
    {
        this.popup = popup;
        thisHuman = human;
    }


}
