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
    public CinemachineVirtualCamera highCamera;
    public GameObject startCanvas;
    public GameObject stageHighCanvas;
    public GameObject stageCanvas;
    public TextMeshProUGUI stageText;
    public Button stageStartBtn;
    public bool[] stageLocked;
    public GameObject LockerImage;
    public Button[] stageButtons;
    
    private int currentStageIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DontDestroyOnLoad(Instance.gameObject);
            Instance = this;
        }
    }

    void Start()
    {
        SoundManager.Instance.PlayStartSound();
        mainCamera.Priority = 10;
        highCamera.Priority = 0;
        foreach (var camera in stageCameras)
        {
            camera.Priority = 0;
        }
        startCanvas.SetActive(true);
        stageCanvas.SetActive(false);
        stageHighCanvas.SetActive(false);

        stageLocked = new bool[stageCameras.Length];
        stageLocked[6] = false;
        for (int i = 0; i < stageLocked.Length - 1; i++)
        {
            stageLocked[i] = true;
        }

        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageNum = i + 1;
            stageButtons[i].onClick.AddListener(() => CloseToStage(stageNum));
        }
    }

    public void BackToMain()
    {
        SoundManager.Instance.PlayCameraMoveSound();
        mainCamera.Priority = 10;
        highCamera.Priority = 0;
        
        stageHighCanvas.SetActive(false);
        StartCoroutine(WaitCameraBackMain());
    }
    
    public void BackToHighCam()
    {
        SoundManager.Instance.PlayCameraMoveSound();
        highCamera.Priority = 10;
        foreach (var camera in stageCameras)
        {
            camera.Priority = 0;
        }
        
        stageCanvas.SetActive(false);
        StartCoroutine(WaitCameraHigh());
        currentStageIndex = 0;
    }
    
    public void StartGame()
    {
        mainCamera.Priority = 0;
        highCamera.Priority = 10;
        startCanvas.SetActive(false);
        SoundManager.Instance.PlayCameraMoveSound();
        StartCoroutine(WaitCameraHigh());
    }
    
    public void CloseToStage(int stageIndex)
    {
        stageHighCanvas.SetActive(false);
        highCamera.Priority = 0;
        StartCoroutine(WaitCameraZoom());
        SetStage(stageIndex);
    }
    
    public void NextStage()
    {
        if (currentStageIndex < stageCameras.Length - 1)
        {
            currentStageIndex++;
            SetStage(currentStageIndex + 1);
        }
    }

    public void PreviousStage()
    {
        if (currentStageIndex > 0)
        {
            currentStageIndex--;
            SetStage(currentStageIndex + 1);
        }
    }

    public void StartStage()
    {
        SoundManager.Instance.StopStartSound();
        StartCoroutine(LoadSceneCoroutine());
    }
    
    IEnumerator LoadSceneCoroutine()
    {
        yield return SceneManager.LoadSceneAsync("SampleScene_Terrian");
    }
    
    void SetStage(int stageNum)
    {
        for (int i = 0; i < stageCameras.Length; i++)
        {
            stageCameras[i].Priority = (i == stageNum - 1) ? 10 : 0;
        }

        if (stageText != null)
        {
            stageText.text = "스테이지 " + (stageNum);
        }

        currentStageIndex = stageNum - 1;
        
        UpdateStageButton(stageNum);
        
        SoundManager.Instance.PlayCameraMoveSound();
    }

    void UpdateStageButton(int stageNum)
    {
        bool isLocked = stageLocked[stageNum - 1];
        stageStartBtn.interactable = !isLocked;
        LockerImage.SetActive(isLocked);
        
        ColorBlock colors = stageStartBtn.colors;
        colors.normalColor = isLocked ? Color.gray : Color.white;
        stageStartBtn.colors = colors;
    }

    IEnumerator WaitCameraHigh()
    {
        stageHighCanvas.SetActive(false);
        yield return new WaitForSeconds(2f);
        stageHighCanvas.SetActive(true);
    }

    IEnumerator WaitCameraZoom()
    {
        stageCanvas.SetActive(false);
        yield return new WaitForSeconds(1f);
        stageCanvas.SetActive(true);
    }
    
    IEnumerator WaitCameraBackMain()
    {
        startCanvas.SetActive(false);
        yield return new WaitForSeconds(2f);
        startCanvas.SetActive(true);
    }
}
