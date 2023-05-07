using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume(float masterVolume)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(masterVolume) * 20);
    }
}
