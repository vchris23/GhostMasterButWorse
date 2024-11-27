using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HumanName", menuName = "Scriptable Objects/Human", order = 1)]
public class ScritableHuman : ScriptableObject

{
    public new string name;
    public float terror;
    public float belief;
    public Fears conciousFear;
    public Fears unconciousFear;
}
