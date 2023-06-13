using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSetup : MonoBehaviour
{
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private AudioMixer masterMixer;

    public void Start()
    {
        SetMasterVolume();
    }

    public void SetMasterVolume()
    {
        float playerPrefsVolume = PlayerPrefs.GetFloat("volume");
        float mappedVolume = Remap(playerPrefsVolume, 0f, 100f, -50f, 5f);

        if (playerPrefsVolume == 0) mappedVolume = -80;
        masterMixer.SetFloat("MasterVolume", mappedVolume);
    }

    float Remap(float value, float originalMin, float originalMax, float newMin, float newMax)
    {
        return (value - originalMin) * (newMax - newMin) / (originalMax - originalMin) + newMin;
    }

}
