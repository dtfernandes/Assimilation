using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsSlider : MonoBehaviour
{

    private Slider _slider;

    [SerializeField]
    private string _playerPref;

    [SerializeField]
    private TextMeshProUGUI _text;

    [SerializeField]
    private char _extraChar;

    // Start is called before the first frame update
    void Awake()
    {
        _slider = GetComponent<Slider>();

        _slider.value = PlayerPrefs.GetFloat(_playerPref);
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = _slider.value + "" + _extraChar; 
        PlayerPrefs.SetFloat(_playerPref, _slider.value);
    }
}
