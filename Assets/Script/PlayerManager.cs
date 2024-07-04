using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Character
{
    None,
    Red,
    Yellow,
    Blue
}
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public GameObject redPrefab;
    public GameObject yellowPrefab;
    public GameObject bluePrefab;
    public Canvas WorldUICanvas;

    public Trajectory trajectory;

    private List<GameObject> players;
    private int currentPlayerIndex = -1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializePlayers();
        ActivateNextCharacter();
    }

    void InitializePlayers()
    {
        players = new List<GameObject>
        {
            Instantiate(redPrefab),
            Instantiate(yellowPrefab),
            Instantiate(bluePrefab)
        };

        foreach (var player in players)
        {
            player.SetActive(false);
        }
    }

    public void ActivateNextCharacter()
    {
        if (currentPlayerIndex < players.Count - 1)
        {
            currentPlayerIndex++;
            GameObject nextPlayer = players[currentPlayerIndex];
            nextPlayer.SetActive(true);
            nextPlayer.transform.position = new Vector3(0.25f, 2f, 0f);
            nextPlayer.transform.rotation = Quaternion.Euler(-35f, 90f, 0f);
        }
        else
        {
            Debug.Log("더 이상 활성화할 캐릭터가 없습니다");
        }
    }

    public void DeactivateCurrentCharacter()
    {
        if (currentPlayerIndex >= 0 && currentPlayerIndex < players.Count)
        {
            GameObject currentPlayer = players[currentPlayerIndex];
            currentPlayer.SetActive(false);
            ActivateNextCharacter();
        }
    }

    
    public void ShowTrajectory(Vector3 startPoint, Vector3 force, float mass)
    {
        trajectory.ShowTrajectory(startPoint, force, mass);
    }

    public void DestroyTrajectory()
    {
        trajectory.DestroyTrajectory();
    }
}
