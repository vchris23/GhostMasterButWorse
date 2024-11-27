using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{

    private ParticleSystem _particleSystem;
    private Human _human;

    private ParticleSystem.MainModule main;
    
    [SerializeField] private Color beliefColor;
    [SerializeField] private Color damageColor;
    

    private void Awake()
    {
        transform.parent.GetComponent<Human>();
        _particleSystem = GetComponent<ParticleSystem>();
        
    }

    private void PlayEffect()
    {
        _particleSystem.Play();
        main.startColor = beliefColor;
    }
    
}
