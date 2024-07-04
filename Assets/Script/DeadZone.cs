using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class DeadZone : MonoBehaviour
{ 
    private void Start()
    {
        if (PlayerManager.Instance == null)
        {
            Debug.LogError("PlayerManager 찾을 수 없음");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger에 오브젝트 들어옴");
        HandleObjectDestruction(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collider에 오브젝트 들어옴");
        HandleObjectDestruction(other.gameObject);
    }

    private void HandleObjectDestruction(GameObject obj)
    {
        PlayerController playerController = obj.GetComponent<PlayerController>();
        if (playerController != null)
        {
            PlayerManager.Instance.DeactivateCurrentCharacter();
        }
        else
        {
            Destroy(obj);
        }
    }
}
