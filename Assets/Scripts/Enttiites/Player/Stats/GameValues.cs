using UnityEngine;


/// <summary>
/// List of values that can be change by Upgrades/Degrades
/// </summary>
[CreateAssetMenu(menuName = "Scriptables/GameValues")]
public class GameValues: ScriptableObject
{

    #region Player

    [field: SerializeField]
    public ScriptableInt P_MaxHealth { get; private set; }

    [field: SerializeField]
    public ScriptableInt P_Speed { get; private set; }

    [field: SerializeField]
    public ScriptableInt P_Attack { get; private set; }


    [field: SerializeField]
    public ScriptableInt P_MaxJumps { get; private set; }

    #endregion

    #region World

    [field: SerializeField]
    public ScriptableInt EndDistance { get; private set; }

    [field: SerializeField]
    public ScriptableInt WorldSize { get; private set; }

    #endregion

    #region Enemies

    #endregion
}