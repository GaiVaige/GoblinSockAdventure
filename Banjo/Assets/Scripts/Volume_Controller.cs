using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume_Controller : MonoBehaviour
{

    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    public void SetMusicLevel (float sliderValue)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
    }

    public void SetSFXLevel(float sliderValue)
    {
        sfxMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
    }

}
