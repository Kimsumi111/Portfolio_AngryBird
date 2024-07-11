using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Editor;
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

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collider에 오브젝트 들어옴");
        HandleObjectDestruction(other.gameObject);
    }

    private void HandleObjectDestruction(GameObject obj)
    {
        IDestroyPortal destroyPortal = obj.GetComponent<IDestroyPortal>();
        PlayerController playerController = obj.GetComponent<PlayerController>();
        if (destroyPortal != null)
        {
            destroyPortal.DestroyPortal();
        }

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
