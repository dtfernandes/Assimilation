using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/GameState")]
public class GameState : ScriptableSingletonObject<GameState>
{
    public void Reset()
    {
        _health.Value = GameValues.P_MaxHealth.Value;
        Floor = 0;
        IsWorldStopped = false;
        GameValues.ResetAll();
    }

    public int Floor { get; set; }
    public bool IsWorldStopped { get; set; }

    [field: SerializeField]
    public ScriptableInt _health;

    [field:SerializeField]
    public GameValues GameValues {get; private set;}

    [field: SerializeField]
    public List<UpgradeDefinition> UpgradeDefinitions { get; private set; }
}
