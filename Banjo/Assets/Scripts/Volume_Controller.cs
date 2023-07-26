using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Volume_Controller : MonoBehaviour
{

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private TMP_Text musicSliderText;
    [SerializeField] private TMP_Text sfxSliderText;

    public const string music_Mixer = "MusicVolume";
    public const string sfx_Mixer = "sfxVolume";

    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(Volume_Controller.music_Mixer, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(Volume_Controller.sfx_Mixer, 1f);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(Volume_Controller.music_Mixer, musicSlider.value);
        PlayerPrefs.SetFloat(Volume_Controller.sfx_Mixer, sfxSlider.value);
    }

    //Sets volume for music mixer
    public void SetMusicLevel (float sliderValue)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
        musicSliderText.text = sliderValue.ToString("0.00");
    }

    //Sets volume for SFX mixer
    public void SetSFXLevel(float sliderValue)
    {
        sfxMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        sfxSliderText.text = sliderValue.ToString("0.00");
    }

}
