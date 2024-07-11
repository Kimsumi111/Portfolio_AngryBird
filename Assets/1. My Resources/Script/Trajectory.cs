using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public int dotCount = 10;   // 궤적의 동그라미 개수
    public float dotSpacing = 0.1f;   // 동그라미 간격
    public float maxSpacing = 1.0f;
    
    private Vector3 startPoint;
    private Vector3 velocity;
    private Vector3 position;
    private float t;
    private Vector3 previousPosition;
    private float spacingInTwo;

    private PlayerController _playerController;

    void Start()
    {
    }

    public void ShowTrajectory(Vector3 startPoint, Vector3 force, float mass)
    {
        this.startPoint = startPoint;
        velocity = force / mass;
        
        
        List<GameObject> dots = new List<GameObject>();
        foreach (Transform dot in transform)    // 자식 객체들 순회
        {
            dots.Add(dot.gameObject);
        }

        for (int i = 0; i < dotCount; i++)
        {
            t = i * dotSpacing;
            position = startPoint + velocity * t + Physics.gravity * (0.5f * t * t);

            if (i > 0)
            {
                previousPosition = dots[i - 1].transform.position;
                spacingInTwo = Vector3.Distance(previousPosition, position);

                if (spacingInTwo > maxSpacing)
                {
                    position = previousPosition + (position - previousPosition);
                }
            }
            
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
        foreach (Transform dot in transform)
        {
            dot.gameObject.SetActive(false);
        }
    }
}