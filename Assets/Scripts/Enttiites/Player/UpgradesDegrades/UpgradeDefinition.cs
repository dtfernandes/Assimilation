using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Upgrades")]
public class UpgradeDefinition : ScriptableObject
{
    [field: SerializeField]
    public string Title { get; private set; }

    [field: SerializeField]
    public string Description { get; private set; }

    [field: SerializeField]
    public UpgradeType Type { get; private set; }

    [field: SerializeField]
    public Rarity Rarity { get; private set; }

    [field: SerializeField]
    public ScriptableInt GameValue { get; private set; }

    [field: SerializeField]
    public int Value { get; private set; }


}
