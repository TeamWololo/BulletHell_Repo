using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderUpdate : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioMixer audioMixer;
    void OnSceneLoaded()
    {
        slider.value = GetMasterLevel();
    }

    public float GetMasterLevel()
    {
        float value;
        bool result = audioMixer.GetFloat("masterVolume", out value);
        if (result)
        {
            return value;
        }
        else
        {
            return -60f;
        }
    }
}
