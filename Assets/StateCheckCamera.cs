using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class StateCheckCamera : MonoBehaviour
{
    public CinemachineStateDrivenCamera StateDrivenCamera;

    public void SetNewPlayerAnim(Animator newAnim)
    {
        // 카메라의 애니메이터 타겟 새로 설정
        StateDrivenCamera.m_AnimatedTarget = newAnim;
    }
}
