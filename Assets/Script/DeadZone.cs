using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger에 오브젝트 들어옴");
        Destroy(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collider에 오브젝트 들어옴");
        Destroy(other.gameObject);
    }
}
