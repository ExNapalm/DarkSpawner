using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class VolumeSaver : MonoBehaviour
{
    [SerializeField] private Slider volumeslider = null;
    [SerializeField] private TextMeshProUGUI volumetext = null;


    private void Start()
    {
        LoadValues();
    }
    public void VolumeSlider(float volume)
    {
        volumetext.text = volume.ToString("0.0");
    }

    public void SaveVolume()
    {
        float volumeammount = volumeslider.value;
        PlayerPrefs.SetFloat("VolumeValue" , volumeammount);
        LoadValues();
    }

    private void LoadValues()
    {
        float volumeammount = PlayerPrefs.GetFloat("VolumeValue");
        volumeslider.value = volumeammount;
        AudioListener.volume = volumeammount;
    }

}
