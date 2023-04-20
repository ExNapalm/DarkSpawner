using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    //To Save the Volume Data
    [SerializeField] private Slider MastervSlider = null;
    [SerializeField] private TextMeshProUGUI Mastervoltext = null;
    [SerializeField] private Slider SFXvSlider = null;
    [SerializeField] private TextMeshProUGUI SFXvoltext = null;
    [SerializeField] private Slider MusicvSlider = null;
    [SerializeField] private TextMeshProUGUI Musicvoltext = null;

    [SerializeField] private AudioMixer audioMixer;

    float voluem;

    //Linear Volume Control for Music, Sound Effects, and Both
    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", level);

        Mastervoltext.text = audioMixer.GetFloat("MasterVolume", out var value).ToString();
    }
    public void SetMusicVolume(float level)
    {

        audioMixer.SetFloat("MusicVolume",level);

        Musicvoltext.text = audioMixer.GetFloat("MusicVolume", out var value).ToString();
    }


    public void SetSFXVolume(float level)
    {
        audioMixer.SetFloat("SFXVolume", level);

        SFXvoltext.text = audioMixer.GetFloat("SFXVolume", out var value).ToString();

    }

    //Start of Saving and Getting Values
    private void Start()
    {
        LoadValues();
    }

    public void SaveVolume()//Save Audio Values
    {
        float MasterVolammount = MastervSlider.value;

        float SFXVolammount = SFXvSlider.value;

        float MusicVolammount = MusicvSlider.value;

        PlayerPrefs.SetFloat("MasterValue", MasterVolammount);
        PlayerPrefs.SetFloat("SFXVolammount", SFXVolammount);
        PlayerPrefs.SetFloat("MusicVolammount", MusicVolammount);
        LoadValues();
    }

    private void LoadValues()//Load all Audio Values
    {
        float MasterVolammount = PlayerPrefs.GetFloat("MasterValue");

        float SFXVolammount = PlayerPrefs.GetFloat("SFXVolammount");

        float MusicVolammount = PlayerPrefs.GetFloat("MusicVolammount");

        MastervSlider.value = MasterVolammount;
        SFXvSlider.value = SFXVolammount;
        MusicvSlider.value = MusicVolammount;
        AudioListener.volume = MasterVolammount;
    }
}
