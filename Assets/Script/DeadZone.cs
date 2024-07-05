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
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collider에 오브젝트 들어옴");
        HandleObjectDestruction(other.gameObject);
    }

    private void HandleObjectDestruction(GameObject obj)
    {
        
        // 캐릭터가 데드존 콜라이더에서 비활성화되었을 때만 더이상 활성화되지 않는다고 뜸.
        
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
