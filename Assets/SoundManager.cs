using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioSource[] _audioSources;

    public AudioClip startSound;
    public AudioClip cameraMoveSound;
    public AudioClip seaSound;
    public AudioClip forestSound;
    public AudioClip backgroundSound;
    public AudioClip fallStar;
    public AudioClip missionClear;
    public AudioClip missionFail;
    public AudioClip bossSound;

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
    
    private void Start()
    {
        _audioSources = GetComponents<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            _audioSources[2].Stop();
            PlayStartSound();
        }
        if (scene.name == "SampleScene")
        {
            _audioSources[0].Stop();
            StartCoroutine(WaitForSound(backgroundSound));
        }
        if (scene.name == "SampleScene_Terrian")
        {
            _audioSources[0].Stop();
            StartCoroutine(WaitForSound(backgroundSound));
        }
    }
    public void PlayStartSound()
    {
        _audioSources[0].clip = startSound;
        _audioSources[0].loop = true;
        _audioSources[0].volume = 0.5f;
        _audioSources[0].Play();
    }

    public void StopStartSound()
    {
        _audioSources[0].Stop();
    }
    
    public void PlayCameraMoveSound()
    {
        _audioSources[1].clip = cameraMoveSound;
        _audioSources[1].loop = false;
        _audioSources[1].volume = 1f;
        _audioSources[1].Play();
    }

    IEnumerator WaitForSound(AudioClip sound)
    {
        _audioSources[2].clip = sound;
        _audioSources[2].loop = true;
        _audioSources[2].volume = 0.5f;
        _audioSources[2].Play();
        yield return new WaitForSeconds(8f);

        _audioSources[3].clip = seaSound;
        _audioSources[3].loop = true;
        _audioSources[3].Play();
        
        _audioSources[4].clip = forestSound;
        _audioSources[4].loop = true;
        _audioSources[4].Play();
    }

    public void PlayStarSound()
    {
        _audioSources[0].loop = false;
        _audioSources[0].PlayOneShot(fallStar, 1f);
    }

    public void PlayMissionClearSound()
    {
        _audioSources[1].clip = missionClear;
        _audioSources[1].loop = false;
        _audioSources[1].Play();
    }

    public void PlayMissionFailSound()
    {
        _audioSources[1].clip = missionFail;
        _audioSources[1].loop = false;
        _audioSources[1].Play();
    }
    
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
