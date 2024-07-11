using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    private Vector3 initposition;
    void Start()
    {
        initposition = transform.position;
        transform.DOShakePosition(2f, 2f, 5, 5f, true);
    }

}
