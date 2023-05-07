using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameValueDisplay : MonoBehaviour
{
    [SerializeField]
    private ScriptableInt _value;

    [SerializeField]
    private string _name;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        string color = "FFFFFF";

        if (_value.Value > _value.DefaultValue)
            color = "00FF00FF";
        else if (_value.Value < _value.DefaultValue)
            color = "FF0000FF";

        //Needs to only update when the menu is open instead of every frame
        _text.text = $"<color=#{color}>" + _name + ": " 
            + _value.Value + "</color>|" + _value.DefaultValue;
    }
}
