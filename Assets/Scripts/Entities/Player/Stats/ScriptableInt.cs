using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptables/Int")]
public class ScriptableInt : ScriptableObject
{
    private int _value;

    [SerializeField]
    public int Value 
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            try 
            {
                OnChange?.Invoke();
            }
            catch
            {
                //I wanted to do this on the OnReset method 
                //but its not working
                OnChange = null;
            }
        }
    }

    [field: SerializeField]
    public int DefaultValue { get; set; }

    public System.Action OnChange { get; set; }

}
