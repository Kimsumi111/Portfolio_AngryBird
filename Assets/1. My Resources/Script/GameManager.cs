using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject stageEndCanvas;
    public Image fadeImage;
    public float fadeDuration;
    public ScaleStars scaleStars;
    public GameObject skillBtn;
    private List<HpController> enemies = new List<HpController>();
    
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

        StartCoroutine(FadeIn()); 
    }

    public void RegisterEnemy(HpController enemy)
    {
        enemies.Add(enemy);
    }

    public void UnregisterEnemy(HpController enemy)
    {
        enemies.Remove(enemy);
        CheckEnemies();
    }

    protected void CheckEnemies()
    {
        if (enemies.Count == 0)
        {
            ActivateCanvas();
        }
    }

    protected void ActivateCanvas()
    {
        skillBtn.SetActive(false);
        stageEndCanvas.SetActive(true);
        scaleStars.UpdateStarImages();
    }
    
    public IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1.0f - (elapsedTime / fadeDuration));
            fadeImage.color = color;
            yield return null;
        }
        color.a = 0f;
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(false);
    }

    public IEnumerator FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1f;
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(false);
    }
    
    public void GoBackMain()
    {
        StartCoroutine(LoadMainScene());
    }

    protected IEnumerator LoadMainScene()
    {
        yield return SceneManager.LoadSceneAsync("MainMenu");
    }
}
