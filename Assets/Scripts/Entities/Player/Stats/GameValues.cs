using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


/// <summary>
/// List of values that can be change by Upgrades/Degrades
/// </summary>
[CreateAssetMenu(menuName = "Scriptables/GameValues")]
public class GameValues: ScriptableObject
{
    /// <summary>
    /// Method that resets all values
    /// </summary>
    public void ResetAll()
    {
        List<PropertyInfo> values = GetAllValues();

        foreach(PropertyInfo v in values)
        {
            ScriptableInt scriptableInt = (ScriptableInt)v.GetValue(this);
            scriptableInt.OnChange = null;
            scriptableInt.Value = scriptableInt.DefaultValue;
            v.SetValue(this, scriptableInt);
        }
    }

    //Gets all ScriptableInts using Reflection
    //It's just easier to scale the code this way
    private static List<PropertyInfo> GetAllValues()
    {
        List<PropertyInfo> scriptableIntProperties = new List<PropertyInfo>();
        PropertyInfo[] properties = typeof(GameValues).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            if (property.PropertyType == typeof(ScriptableInt))
            {
                scriptableIntProperties.Add(property);
            }
        }

        return scriptableIntProperties;
    }

    #region Player

    [field: SerializeField]
    public ScriptableInt Level { get; private set; }

    [field: SerializeField]
    public ScriptableInt Exp { get; private set; }


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
    [field: SerializeField]
    public ScriptableInt E_MaxHealth { get; private set; }

    [field: SerializeField]
    public ScriptableInt E_Speed { get; private set; }

    [field: SerializeField]
    public ScriptableInt E_Attack { get; private set; }
    #endregion
}