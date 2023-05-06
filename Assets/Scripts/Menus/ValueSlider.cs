using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ValueSlider : MonoBehaviour
{
    [SerializeField]
    private ScriptableInt _value;

    private Slider _slider;
   

    void Awake()
    {
        _slider = GetComponent<Slider>();
        LevelUp.Level = 0;
        _slider.maxValue = LevelUp.levelLimits[LevelUp.Level];
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = _value.Value;


        if(_value.Value >= _slider.maxValue)
        {
            _value.Value = 0;
            LevelUp.Level++;
            _slider.maxValue = LevelUp.levelLimits[LevelUp.Level];
        }
    }
}
