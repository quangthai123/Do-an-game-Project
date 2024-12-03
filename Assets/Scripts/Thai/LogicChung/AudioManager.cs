using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource currentWordAudio;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    public void SetCurrentWordAudio(AudioClip _audio) 
    {
        this.currentWordAudio.clip = _audio;
        currentWordAudio.playOnAwake = false;
    }
    public void PlayCurrentWordAudio() => currentWordAudio.Play();
}
