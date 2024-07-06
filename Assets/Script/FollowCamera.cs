using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class FollowCamera : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    public Transform currentTarget;
    private GameObject newPlayer;
    readonly string[] playerTags = { "RedPlayer", "YellowPlayer", "BluePlayer" };
    
    void Start()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnCharacterChanged += UpdateCameraTarget;
        }
        FindNewTarget();
    }
    
    void Update()
    {
        if (currentTarget == null)
        {
            FindNewTarget();
        }
    }

    public void FindNewTarget()
    {
        newPlayer = FindNewPlayer();
        if (newPlayer != null)
        {
            currentTarget = newPlayer.transform;
            VirtualCamera.Follow = currentTarget;
            VirtualCamera.LookAt = currentTarget;
        }
    }
    
    GameObject FindNewPlayer()
    {
        foreach (string tag in playerTags)
        {
            GameObject player = GameObject.FindWithTag(tag);
            if (player != null && player.activeInHierarchy)
            {
                return player;
            }
        }

        return null;
    }

    void UpdateCameraTarget(Transform newTarget)
    {
        if (newTarget != null)
        {
            currentTarget = newTarget;
            VirtualCamera.Follow = currentTarget;
            VirtualCamera.LookAt = currentTarget;
        }
    }
}
