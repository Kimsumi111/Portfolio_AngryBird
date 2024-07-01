using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.PlayerLoop;

public class FollowCamera : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    private Transform currentTarget;
    // public float followDistance = 10f;
    // public Vector2 xLimits;
    // public Vector2 yLimits;
    
    void Start()
    {
        FindNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        // if (currentTarget != null)
        // {
        //     Vector3 targetPosition = currentTarget.position;
        //     targetPosition.x = Mathf.Clamp(targetPosition.x, xLimits.x, xLimits.y);
        //     targetPosition.y = Mathf.Clamp(targetPosition.y, yLimits.x, yLimits.y);
        //
        //     VirtualCamera.transform.position = targetPosition + currentTarget.right * followDistance;
        //     VirtualCamera.transform.LookAt(currentTarget);
        // }
        if (currentTarget == null)
        {
            FindNewTarget();
        }
    }

    void FindNewTarget()
    {
        GameObject newPlayer = FindNewPlayer();
        if (newPlayer != null)
        {
            currentTarget = newPlayer.transform;
            VirtualCamera.Follow = currentTarget;
            VirtualCamera.LookAt = currentTarget;
        }
    }
    GameObject FindNewPlayer()
    {
        return GameObject.FindWithTag("Player");
    }
    
}
