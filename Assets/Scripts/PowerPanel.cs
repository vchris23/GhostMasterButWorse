using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerPanel : MonoBehaviour
{
    private Transform[] children;
    private Button[] buttons;
    private Image[] cooldownBars;
    private Ghost ghost;

    [SerializeField] private Color usingAbilityColor;
    [SerializeField] private Color notUsingAbilityColor;
    public Ghost Ghost
    {
        set
        {
            ghost = value;
            abilities = value._abilities;
        }
    }
    private Ability[] abilities;
    // Start is called before the first frame update
    void Awake()
    {
        children = new Transform[transform.childCount];
        buttons = new Button[transform.childCount];
        cooldownBars = new Image[transform.childCount];
        
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
            buttons[i] = children[i].GetComponent<Button>();
            cooldownBars[i] = children[i].gameObject.transform.GetChild(0).GetComponent<Image>();
            print(cooldownBars[i].name);
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = children[i];
            
            if (i == 0)
            {
                child.gameObject.SetActive(true);
                child.GetComponentInChildren<TMP_Text>().text = "Nothing";
                child.GetComponent<Button>().onClick.RemoveAllListeners();
                child.GetComponent<Button>().onClick.AddListener(delegate {ChangeAbility(0);});
                continue;
            }
            
            if (i <= abilities.Length)
            {
                child.gameObject.SetActive(true);
                TMP_Text[] texts = child.GetComponentsInChildren<TMP_Text>();
                print("Ability at index 0... " + abilities[0].name);
                texts[0].text = abilities[i-1].name;
                texts[1].text = "cost: " + abilities[i-1].PlasmaCost;
                int number = i;
                print($"Setting button of {abilities[i - 1].name} with number {number}");
                
                child.GetComponent<Button>().onClick.RemoveAllListeners();
                child.GetComponent<Button>().onClick.AddListener(delegate {ChangeAbility(number);});
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            cooldownBars[i + 1].fillAmount = 1 - (abilities[i].CurrentCooldown / abilities[i].Cooldown);
        }
    }

    public void ChangeAbility(int index)
    {
        int indexDiff = index - ghost.AbilitySelected;
        if (indexDiff > 0 && index != 0)
        {
            print("Index difference " + indexDiff);
            int addedCost = 0;
            for (int i = ghost.AbilitySelected; i < index; i++)
            {
                print("Summed ability: " + abilities[i].name);
                addedCost += abilities[i].PlasmaCost;
            }
            if (addedCost + GhostManager.plasmaUse > GhostManager.maxPlasma)
            {
                print("Insufficient plasma");
                return;
            }
        }

        print("changed ghost ability");
        print("ability index: " + index);
        ghost.AbilitySelected = index;
        
        for (int i = 0; i <= abilities.Length; i++)
        {
            if (i <= ghost.AbilitySelected)
            {
                buttons[i].image.color = usingAbilityColor;
            }
            else
            {
                buttons[i].image.color = notUsingAbilityColor;
            }
            
        }
    }
}
