using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoop : MonoBehaviour
{
    AudioSource musicSource;
    [SerializeField] AudioClip musicStart;

    void Awake() 
    {
        musicSource = FindObjectOfType<AudioSource>();
    }

    void Start()
    {
        musicSource.PlayOneShot(musicStart);
        musicSource.PlayScheduled(AudioSettings.dspTime + musicStart.length);
    }
}
