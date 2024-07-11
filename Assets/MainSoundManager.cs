using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundManager : MonoBehaviour
{
    public static MainSoundManager Instance { get; private set; }
    
    public AudioSource cameraMoveAudio;
    public AudioSource backGroundAudio;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlayBackgroundSound()
    {
        backGroundAudio.Play();
    }
    
    public void StopBackgroundSound()
    {
        backGroundAudio.Stop();
    }

    public void PlayCameraMoveSound()
    {
        cameraMoveAudio.Play();
    }
}
