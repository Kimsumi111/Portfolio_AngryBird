using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class MainSceneManager : MonoBehaviour
{
    public static MainSceneManager Instance { get; private set; }

    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera[] stageCameras;
    public GameObject startCanvas;
    public GameObject stageCanvas;
    public TextMeshProUGUI stageText;
    public Button stageStartBtn;
    public bool[] stageLocked;
    public GameObject LockerImage;

    public MainSoundManager _mainSoundManager;
    
    private int currentStage = 0;

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

    void Start()
    {
        mainCamera.Priority = 10;
        foreach (var camera in stageCameras)
        {
            camera.Priority = 0;
        }
        startCanvas.SetActive(true);
        stageCanvas.SetActive(false);

        stageLocked = new bool[stageCameras.Length];
        stageLocked[0] = false;
        for (int i = 1; i < stageLocked.Length; i++)
        {
            stageLocked[i] = true;
        }
        
        UpdateStageButton();
    }

    public void StartGame()
    {
        startCanvas.SetActive(false);
        stageCanvas.SetActive(true);

        currentStage = 0;
        SetStage(currentStage);
    }
    
    public void NextStage()
    {
        if (currentStage < stageCameras.Length - 1)
        {
            currentStage++;
            SetStage(currentStage);
        }
    }

    public void PreviousStage()
    {
        if (currentStage > 0)
        {
            currentStage--;
            SetStage(currentStage);
        }
    }

    public void StartStage()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    void SetStage(int stageIndex)
    {
        mainCamera.Priority = 0;
        for (int i = 0; i < stageCameras.Length; i++)
        {
            stageCameras[i].Priority = (i == stageIndex) ? 10 : 0;
        }

        if (stageText != null)
        {
            stageText.text = "스테이지 " + (stageIndex + 1);
        }
        
        UpdateStageButton();
        
        _mainSoundManager.PlayCameraMoveSound();
    }

    void UpdateStageButton()
    {
        bool isLocked = stageLocked[currentStage];
        stageStartBtn.interactable = !isLocked;
        LockerImage.SetActive(isLocked);
        
        ColorBlock colors = stageStartBtn.colors;
        colors.normalColor = isLocked ? Color.gray : Color.white;
        stageStartBtn.colors = colors;
    }

    IEnumerator WaitCameraMove()
    {
        stageCanvas.SetActive(false);
        yield return new WaitForSeconds(1f);
        stageCanvas.SetActive(true);
    }
}
