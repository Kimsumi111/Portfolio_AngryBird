using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    public Material dayMat;
    public Material nightMat;
    public GameObject dayLight;
    public GameObject nightLight;

    public Color dayFog;
    public Color nightFog;
    
    private void Update()
    {
        // 하늘 흘러감
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.5f);
    }

    // void OnGUI()
    // {
    //     // 낮과 밤 조종
    //     if (GUI.Button(new Rect(5, 5, 80, 20), "Day"))
    //     {
    //         RenderSettings.skybox = dayMat;
    //         RenderSettings.fogColor = dayFog;
    //         RenderSettings.fog = false;
    //         dayLight.SetActive(true);  
    //         nightLight.SetActive(false);
    //     }
    //     
    //     if (GUI.Button(new Rect(5, 35, 80, 20), "Night"))
    //     {
    //         RenderSettings.skybox = nightMat;
    //         RenderSettings.fog = true;
    //         RenderSettings.fogColor = nightFog;
    //         dayLight.SetActive(false);  
    //         nightLight.SetActive(true);
    //     }
    // }
        
}
