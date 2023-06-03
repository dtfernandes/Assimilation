using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/BiomeConfig")]
public class BiomeConfig : ScriptableObject
{
    [SerializeField]
    private Biome _biome;
    public Biome Biome { get => _biome; }

   [field: SerializeField]
   public Sprite Background {get; private set; }

}

