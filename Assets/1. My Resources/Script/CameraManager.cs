using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }
    [SerializeField]
    private Animator playerAnimator;
    public string targetAnimationState1;
    public string targetAnimationState2;
    private CinemachineBrain cinemachineBrain;
    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera landscapeCamera1;
    public CinemachineDollyCart landscape2dollyCart;
    public CinemachineVirtualCamera landscapeCamera2;
    public CinemachineVirtualCamera shotCamera;

    protected virtual void Awake()
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
        
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        
        if (cinemachineBrain == null)
        {
            Debug.LogError("Main Camera에 CinemachineBrain 컴포넌트가 없음.");
        }
    }

    protected virtual void Start()
    {
        landscapeCamera1.Priority = 10;
        landscapeCamera2.Priority = 0;
        mainCamera.Priority = 0;
        shotCamera.Priority = 0;

        StartCoroutine(ShowLandscape());
    }

    protected virtual void Update()
    {
        // DeadZone 콜라이더에 부딪혀 비활성화 될 경우 카메라 매니저의 애니메이터가 변경되지 않음.
        if (playerAnimator == null)
        {
            return;
        }

        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(targetAnimationState1))
        {
            mainCamera.Priority = 0;
            shotCamera.Priority = 10;
        }
        else if (stateInfo.IsName(targetAnimationState2))
        {
            mainCamera.Priority = 10;
            shotCamera.Priority = 0;
        }
    }
    
    public void SetNewPlayeranim(Animator newAnimtor)
    {
        playerAnimator = newAnimtor;
        Debug.Log($"New playerAnimator 설정됨 : {playerAnimator}");
    }

    IEnumerator ShowLandscape()
    {
        yield return new WaitForSeconds(4f);

        landscapeCamera1.gameObject.SetActive(false);
        landscape2dollyCart.gameObject.SetActive(true);
        landscapeCamera2.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(4f);
    
        landscapeCamera2.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        shotCamera.gameObject.SetActive(true);
        
        PlayerManager.Instance.ActiveButton();
    }
}
