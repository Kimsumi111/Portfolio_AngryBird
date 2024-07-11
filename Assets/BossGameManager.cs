using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossGameManager : GameManager
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void GoBossStage()
    {
        StartCoroutine(LoadBossScene());
    }

    protected IEnumerator LoadBossScene()
    {
        yield return SceneManager.LoadSceneAsync("SampleScene");
    }
}
