using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MyText : MonoBehaviour
{
    void Start()
    {
        Text txt = GetComponent<Text>();
        txt.DOText("DOTween", 2, true, ScrambleMode.All).SetDelay(1);
        // 특수한 옵션(매개변수)가 달려있는 경우도 있음.

        
        // RectTransform rt = GetComponent<RectTransform>();
        // rt.DOAnchorPosY(100, 1).SetDelay(1.5f).SetEase(Ease.InOutBounce);
        // DOAnchorPosY: Anchor 중 Y의 위치 변경
        // SetDelay : 원하는 초 후, 동작 실행.   <= 시퀀스
        // Ease : 애니메이션 완화 효과가 담긴 클래스. 다양한 애니메이션 효과 낼 수 있음.
        //        In 시작 완화, Out 끝 완화
    }
}
