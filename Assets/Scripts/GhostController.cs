using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GhostController : MonoBehaviour
{
    private Button Bind;
    private PowerPanel _powerMenu;
    private Button bench;
    [SerializeField] private MouseSelector ghostSpawner;

    private void Awake()
    {
        Bind = transform.GetChild(1).GetComponent<Button>();
        bench = transform.GetChild(3).GetComponent<Button>();
        _powerMenu = transform.GetChild(2).GetChild(1).GetComponent<PowerPanel>();
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    public void ChangeGhost(Ghost ghost)
    {
        Bind.onClick.RemoveAllListeners();
        Bind.onClick.AddListener(delegate {
            if (!ghost.isActiveAndEnabled && ghost.getAbilityCost() + GhostManager.plasmaUse > GhostManager.maxPlasma) return; 
            
            ghostSpawner._currentSelectedGhost = ghost;
        });
        Bind.onClick.AddListener(delegate
        {
            if (!ghost.isActiveAndEnabled && ghost.getAbilityCost() + GhostManager.plasmaUse > GhostManager.maxPlasma) return; 
            gameObject.SetActive(false);
        });
        bench.onClick.AddListener(ghost.bench);
        _powerMenu.gameObject.SetActive(false);
        _powerMenu.Ghost = ghost;
    }
    

    private void OnDisable()
    {
        _powerMenu.gameObject.SetActive(false);
    }
}
