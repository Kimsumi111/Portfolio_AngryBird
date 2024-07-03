using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
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
        landscapeCamera1.Priority = 10;
        landscapeCamera2.Priority = 0;
        mainCamera.Priority = 0;
        shotCamera.Priority = 0;

        // StartCoroutine(ShowLandscape());
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
    }
}
