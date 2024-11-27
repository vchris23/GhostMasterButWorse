using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlasmaBar : MonoBehaviour
{
    [SerializeField] private TMP_Text maxText;
    [SerializeField] private TMP_Text inUseText;
    [SerializeField] private Image maxBar;
    [SerializeField] private Image inUseBar;

    private void OnEnable()
    {
        GhostManager.OnPlasmaRecalculated += UpdateInfo;
        UpdateInfo();
    }

    private void OnDisable()
    {
        GhostManager.OnPlasmaRecalculated += UpdateInfo;
    }

    void UpdateInfo()
    {
        maxText.text = GhostManager.maxPlasma.ToString();
        inUseText.text = GhostManager.plasmaUse.ToString();
        maxBar.fillAmount = GhostManager.maxPlasma * 0.001f;
        inUseBar.fillAmount = GhostManager.plasmaUse * 0.001f;
    }
}
