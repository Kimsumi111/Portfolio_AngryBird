using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSources;
    
    public AudioClip seaSound;
    public AudioClip forestSound;
    public AudioClip backgroundSound;

    private void Start()
    {
        _audioSources = GetComponents<AudioSource>();

        StartCoroutine(WaitForSound());
    }

    IEnumerator WaitForSound()
    {
        _audioSources[0].clip = backgroundSound;
        _audioSources[0].loop = true;
        _audioSources[0].volume = 1f;
        _audioSources[0].Play();
        yield return new WaitForSeconds(8f);

        _audioSources[0].volume = 0.3f;
        
        _audioSources[1].clip = seaSound;
        _audioSources[1].loop = true;
        _audioSources[1].Play();
        
        _audioSources[2].clip = forestSound;
        _audioSources[2].loop = true;
        _audioSources[2].Play();
    }
}
