using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text scoreText;
    public int score;
    
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DontDestroyOnLoad(Instance.gameObject);
            Instance = this;
        }
    }

    protected virtual void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;

        if (gameObject.IsDestroyed())
            return;
        
        CustomEvent.Trigger(gameObject, "TriggerScoreSeq", points);
    }

    protected void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
