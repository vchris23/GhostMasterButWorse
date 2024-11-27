using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class humanPopup : MonoBehaviour
{
    private RectTransform panelTransform;
    [SerializeField] private TMP_Text name;
    [SerializeField] private Image beliefBar;
    [SerializeField] private Image terrorBar;

    private void Awake()
    {
        panelTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        panelTransform.position = Input.mousePosition;

        if (HumanPanelController.selectedHuman)
        {
            beliefBar.fillAmount = HumanPanelController.selectedHuman.belief / 10;
            terrorBar.fillAmount = HumanPanelController.selectedHuman.terror / 100;
        }

    }

    private void OnEnable()
    {
        if (HumanPanelController.selectedHuman)
        {
            name.text = HumanPanelController.selectedHuman.name;
        }
    }
}
