using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GhostPanel : MonoBehaviour
{
    [SerializeField] private GhostController ghostController;
    
    
    public void PopulatePanel(Ghost[] ghosts)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform ghostButton = transform.GetChild(i);
            if (i < ghosts.Length)
            {
                ghostButton.gameObject.gameObject.SetActive(true);
                TMP_Text ghostName = ghostButton.GetComponentInChildren<TMP_Text>();
                Ghost ghost = ghosts[i];
                ghostButton.GetComponent<Button>().onClick.AddListener(delegate { ghostController.ChangeGhost(ghost);});
                ghostName.text = ghost.name;
            }
            else
            {
                ghostButton.gameObject.SetActive(false);
            }

        }
    }
    
}
