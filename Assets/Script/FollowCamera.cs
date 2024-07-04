using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.PlayerLoop;

public class FollowCamera : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    private Transform currentTarget;
    
    void Start()
    {
        FindNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
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
        GameObject player = GameObject.FindWithTag("RedPlayer");
        if (player != null)
        {
            return player;
        }

        player = GameObject.FindWithTag("YellowPlayer");
        if (player != null)
        {
            return player;
        }

        player = GameObject.FindWithTag("RedPlayer");
        if (player != null)
        {
            return player;
        }

        return null;
    }
    
}
