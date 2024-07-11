using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cube : MonoBehaviour
{
    void Start()
    {
        transform.DOMove(Vector3.up, 5);
        transform.DOScale(Vector3.one * 3, 5);
        transform.DORotate(Vector3.forward, 5);
        
        Material mat = GetComponent<MeshRenderer>().material;
        mat.DOColor(Color.green, 5);
    }

}
