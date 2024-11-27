using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[Serializable]
public class MouseSelector : MonoBehaviour
{
    private static Entity _currentSelectedFetter;

    public static Entity CurrentSelectedFetter
    {
        get { return _currentSelectedFetter; }
        set { _currentSelectedFetter = value; }
    }

    public Ghost _currentSelectedGhost;

    public Ghost CurrentSelectedGhost
    {
        get => _currentSelectedGhost;
        set => _currentSelectedGhost = value;
    }

    [SerializeField] private TMP_Text entityTooltip;
    private bool hasGhostSelected = true;

    private void Update()
    {
        if (_currentSelectedFetter && _currentSelectedFetter.fetter != Fetter.None)
        {
            entityTooltip.text = _currentSelectedFetter.fetter.ToString();
            entityTooltip.rectTransform.position = Camera.main.WorldToScreenPoint(_currentSelectedFetter.transform.position);
            entityTooltip.gameObject.SetActive(true);
            if (_currentSelectedGhost)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    print("Mouse 0 down");
                    if (_currentSelectedGhost.changeEntity(_currentSelectedFetter)) _currentSelectedGhost = null;
                }
            }
        }
        else
        {
            entityTooltip.gameObject.SetActive(false);
        }

        if (CurrentSelectedGhost)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _currentSelectedGhost = null;
            }
        }

    }

    private void FixedUpdate()
    {
        if (_currentSelectedGhost)
        {
            hasGhostSelected = true;
            GameObject[] potentialFetters = GameObject.FindGameObjectsWithTag("Entity");
            foreach (GameObject fetter in potentialFetters)
            {
                Entity script = fetter.GetComponent<Entity>();
                if (_currentSelectedGhost.Fetters.Contains(script.fetter) && !script.currentConnectedGhost)
                {
                    script.Highlight();
                }
            }
        }
        else if (hasGhostSelected == true)
        {
            hasGhostSelected = false;
            GameObject[] potentialFetters = GameObject.FindGameObjectsWithTag("Entity");
            foreach (GameObject fetter in potentialFetters)
            {
                Entity script = fetter.GetComponent<Entity>();
                script.UnHighlight();
            }
        }
    }
}