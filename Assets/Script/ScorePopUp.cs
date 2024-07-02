using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ScorePopUp : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public float duration = 1f;
    public float moveSpeed = 2f;
    public Color startColor;
    public Color endColor;

    private float timer;

    public void Setup(int score)
    {
        scoreText.text = score.ToString();
        scoreText.color = startColor;
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > duration)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.up * (moveSpeed * Time.deltaTime));
            scoreText.color = Color.Lerp(startColor, endColor, timer / duration);
        }
    }
}
