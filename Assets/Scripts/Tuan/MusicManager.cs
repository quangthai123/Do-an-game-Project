using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    public AudioClip BgMusic;

    private void Start()
    {
        musicSource.clip = BgMusic;
        musicSource.Play();
    }
}
