using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SlingshotRubberBand : MonoBehaviour
{
    public LineRenderer leftRubber;   // 왼쪽 고무줄
    public LineRenderer rightRubber;  // 오른쪽 고무줄
    public Transform seat;            // 프리팹의 Transform
    public Transform leftAnchor;      // 새총 왼쪽 끝점
    public Transform rightAnchor;     // 새총 오른쪽 끝점

    void Start()
    {
        // LineRenderer의 positionCount를 2로 설정
        leftRubber.positionCount = 2;
        rightRubber.positionCount = 2;
    }

    void Update()
    {
        // 고무줄의 시작점과 끝점을 업데이트
        leftRubber.SetPosition(0, leftAnchor.position);
        leftRubber.SetPosition(1, seat.position);

        rightRubber.SetPosition(0, rightAnchor.position);
        rightRubber.SetPosition(1, seat.position);
    }
}