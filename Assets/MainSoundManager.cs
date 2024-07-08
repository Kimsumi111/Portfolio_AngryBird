using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundManager : MonoBehaviour
{
    public AudioSource cameraMoveAudio;
    
    public void PlayCameraMoveSound()
    {
        cameraMoveAudio.Play();
    }
}
