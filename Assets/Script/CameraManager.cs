using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;
    public string targetAnimationState1;
    public string targetAnimationState2;
    private CinemachineBrain cinemachineBrain;
    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera shotCamera;

    private void Awake()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();

        if (cinemachineBrain == null)
        {
            Debug.LogError("Main Camera에 CinemachineBrain 컴포넌트가 없음.");
            return;
        }
    }

    private void Start()
    {
        mainCamera.Priority = 10;
        shotCamera.Priority = 0;
    }

    private void Update()
    {
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
}
