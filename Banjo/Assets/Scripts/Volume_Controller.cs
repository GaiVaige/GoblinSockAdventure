using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume_Controller : MonoBehaviour
{

    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    //Sets volume for music mixer
    public void SetMusicLevel (float sliderValue)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
    }

    //Sets volume for SFX mixer
    public void SetSFXLevel(float sliderValue)
    {
        sfxMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
    }

}
