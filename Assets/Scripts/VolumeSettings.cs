using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{

    // pause menu volume slider - Aleksi
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider volumeSlider;

    public void SetMusicVolume()
    {
        float volume = volumeSlider.value;
        myMixer.SetFloat("Volume", Mathf.Log10(volume)*25);
    }
}
