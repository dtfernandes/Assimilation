using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Slider))]
public class ValueSlider : MonoBehaviour
{
    [SerializeField]
    private ScriptableInt _value;
    [SerializeField]
    private ScriptableInt _level;
    [SerializeField]
    private ScriptableInt _health;

    [SerializeField]
    private SkillSelection skillSelection;

    [SerializeField]
    private TextMeshProUGUI expText;

    private Slider _slider;
   

    void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = LevelUp.levelLimits[_level.Value];
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = _value.Value;
        expText.text = _slider.value + "/" + _slider.maxValue;

        if (_value.Value >= _slider.maxValue)
        {
            //Reset Health
            _health.Value = 
                GameState.Instance
                .GameValues.P_MaxHealth.Value;

            _value.Value = 0;
            _level.Value++;
            _slider.maxValue = LevelUp.levelLimits[_level.Value];

            skillSelection.gameObject.SetActive(true);
            skillSelection.SetupSelection(UpgradeType.Upgrade);
        }
    }
}
