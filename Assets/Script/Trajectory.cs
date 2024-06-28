using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public int dotCount = 10; // 궤적의 동그라미 개수
    public float dotSpacing = 0.1f; // 동그라미 간격
    
    public List<GameObject> dots;
    private Vector3 startPoint;

    private PlayerController _playerController;

    void Start()
    {
        foreach (Transform dot in transform)    // 자식 객체들 순회
        {
            dots.Add(dot.gameObject);
        }
    }

    public void ShowTrajectory(Vector3 startPoint, Vector3 force, float mass)
    {
        this.startPoint = startPoint;
        Vector3 velocity = force / mass;

        for (int i = 0; i < dotCount; i++)
        {
            float t = i * dotSpacing;
            Vector3 position = startPoint + velocity * t + Physics.gravity * (0.5f * t * t);

            if (i < dots.Count)
            {
                dots[i].transform.position = position;
                dots[i].SetActive(true);
            }
            else
            {
                Debug.LogWarning("dotCount가 dots 리스트보다 더 큼");
                break;
            }
            
        }
    }

    public void DestroyTrajectory()
    {
        foreach (var dot in dots)
        {
            dot.SetActive(false);
        }
    }
}