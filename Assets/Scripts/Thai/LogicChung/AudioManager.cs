using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource currentWordAudio;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        currentWordAudio = GetComponent<AudioSource>();
    }
    public void SetCurrentWordAudio(AudioClip _audio) 
    {
        this.currentWordAudio.clip = _audio;
    }
    public void PlayCurrentWordAudio() => currentWordAudio.Play();
}
