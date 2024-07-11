using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScaleStars : MonoBehaviour
{
    public Image backGround;
    public Image[] starImages;
    public Sprite brightStar;
    public Sprite darkStar;
    public TextMeshProUGUI comment;
    public TextMeshProUGUI totalScore_txt;
    
    public int canGetScore;
    public Vector3 initialScale = new Vector3(0.1f, 0.1f, 0.1f);
    public float duration = 0.3f;
    public ScoreManager scoreManager;

    private int currentScore;
    private int starCount;

    public void UpdateStarImages()
    {
        currentScore = scoreManager.score;
        Debug.Log(scoreManager.score);
        if (currentScore > (canGetScore / 3) * 2)
        {
            starCount = 3;
            comment.text = "대단해요!!!";
        }
        else if (currentScore > canGetScore / 3)
        {
            starCount = 2;
            comment.text = "훌륭해요!!";
        }
        else
        {
            starCount = 1;
            comment.text = "잘했어요!";
        }

        for (int i = 0; i < starImages.Length; i++)
        {
            if (i < starCount)
            {
                starImages[i].sprite = brightStar;
            }
            else
            {
                starImages[i].sprite = darkStar;
            }
        }
        
        foreach (var star in starImages)
        {
            star.rectTransform.localScale = initialScale;
        }
        
        totalScore_txt.text = $"획득 점수 : {currentScore}";
        comment.rectTransform.localScale = initialScale;
        
        SoundManager.Instance.PlayMissionClearSound();
        
        ScaleImage(0);
    }

    public void SetFailStarImage()
    {
        backGround.color = Color.red;
        comment.text = "아쉬워요...";
        totalScore_txt.text = "미션에 실패하였습니다.";
        comment.rectTransform.localScale = initialScale;

        foreach (var star in starImages)
        {
            star.sprite = darkStar;
            star.rectTransform.localScale = initialScale;
        }
        
        SoundManager.Instance.PlayMissionFailSound();
        ScaleFailStarImage();
        comment.rectTransform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
    }

    public void ScaleFailStarImage()
    {
        SoundManager.Instance.PlayStarSound();
        foreach (var star in starImages)
        {
            star.rectTransform.DOScale(Vector3.one, duration).SetEase(Ease.OutBounce);
        }
        
    }
    
    void ScaleImage(int index)
    {
        if (index >= starImages.Length)
        {
            return;
        }

        SoundManager.Instance.PlayStarSound();
        starImages[index].rectTransform.DOScale(Vector3.one, duration).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            ScaleImage(index + 1);
        });

        StartCoroutine(textEffect());
    }

    IEnumerator textEffect()
    {
        
        yield return new WaitForSeconds(1.2f);
        comment.rectTransform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
    }
}
