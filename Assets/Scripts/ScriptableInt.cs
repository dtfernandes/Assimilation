using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptables/Int")]
public class ScriptableInt : ScriptableObject
{
    [field: SerializeField]
    public int Value { get; set; }
}
