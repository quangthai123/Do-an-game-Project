using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControll : MonoBehaviour
{
    public AudioSource SoundBackground;
    public Slider SettingVolume;
    public MusicManager musicManager;
    void Start()
    {
        SoundBackground.Play();
        SettingVolume.value = SoundBackground.volume;
        musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.StopMusic();
        }
    }

    public void ChangeVolume()
    {
        float volume = SettingVolume.value;
        SoundBackground.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

}
