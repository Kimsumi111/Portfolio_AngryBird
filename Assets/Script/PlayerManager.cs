using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    public GameObject Buttons;
    public TextMeshProUGUI redText;
    public TextMeshProUGUI yellowText;
    public TextMeshProUGUI blueText;

    public Trajectory trajectory;
    public FollowCamera followCamera;
    public int redCount = 2;
    public int yellowCount = 1;
    public int blueCount = 1;
    
    private List<GameObject> redList;
    private List<GameObject> yellowList;
    private List<GameObject> blueList;

    private GameObject currentPlayer;
    private Character currentCharacter;

    private Dictionary<Character, int> characterUses;

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
        InitializeCharacterUses();
        UpdateCharacterUsesText();
    }

    void InitializePlayers()
    {
        redList = new List<GameObject>();
        yellowList = new List<GameObject>();
        blueList = new List<GameObject>();
        
        for (int i = 0; i < redCount; i++) // Red
        {
            GameObject player = Instantiate(redPrefab);
            player.SetActive(false);
            redList.Add(player);
        }

        for (int i = 0; i < yellowCount; i++) // Yellow
        {
            GameObject player = Instantiate(yellowPrefab);
            player.SetActive(false);
            yellowList.Add(player);
        }

        for (int i = 0; i < blueCount; i++) // Blue
        {
            GameObject player = Instantiate(bluePrefab);
            player.SetActive(false);
            blueList.Add(player);
        }
    }

    void InitializeCharacterUses()
    {
        characterUses = new Dictionary<Character, int>
        {
            { Character.Red, redList.Count },
            { Character.Yellow, yellowList.Count },
            { Character.Blue, blueList.Count }
        };
    }

    void UpdateCharacterUsesText()
    {
        redText.text = (characterUses[Character.Red]).ToString();
        yellowText.text = (characterUses[Character.Yellow]).ToString();
        blueText.text = (characterUses[Character.Blue]).ToString();
    }

    public void ActivateNextCharacter(Character characterType)
    {
        if (currentPlayer != null)
        {
            currentPlayer.SetActive(false);
        }

        switch (characterType)
        {
            case Character.Red:
                if (redList.Count > 0)
                {
                    currentPlayer = redList[0];
                    currentCharacter = Character.Red;
                }
                break;
            case Character.Yellow:
                if (yellowList.Count > 0)
                {
                    currentPlayer = yellowList[0];
                    currentCharacter = Character.Yellow;
                }
                break;
            case Character.Blue:
                if (blueList.Count > 0)
                {
                    currentPlayer = blueList[0];
                    currentCharacter = Character.Blue;
                }
                break;
        }

        if (currentPlayer != null)
        {
            currentPlayer.SetActive(true);
            currentPlayer.transform.position = new Vector3(0.25f, 2f, 0f);
            currentPlayer.transform.rotation = Quaternion.Euler(-35f, 90f, 0f);

            Rigidbody rigid = currentPlayer.GetComponent<Rigidbody>();
            rigid.constraints = RigidbodyConstraints.FreezeAll;
        }

        else
        {
            Debug.Log("더 이상 활성화할 캐릭터가 없습니다");
        }
    }

    public void DeactivateCurrentCharacter()
    {
        if (currentPlayer != null)
        {
            currentPlayer.SetActive(false);
            currentPlayer = null;
            followCamera.currentTarget = null;
        }
        ActivateRandomCharacter();
        ActiveButton();
    }

    public void DownCharacterCount(Character characterType)
    {
        if (characterUses.ContainsKey(characterType))
        {
            characterUses[characterType]--;
            UpdateCharacterUsesText();
        }
    }

    public void ActivateRandomCharacter()
    {
        List<Character> availableCharacters = new List<Character>();
        
        if (characterUses[Character.Red] != 0)
        {
            availableCharacters.Add(Character.Red);
        }
        if (characterUses[Character.Yellow] != 0)
        {
            availableCharacters.Add(Character.Yellow);
        }
        if (characterUses[Character.Blue] != 0)
        {
            availableCharacters.Add(Character.Blue);
        }

        if (availableCharacters.Count > 0)
        {
            int randomIndex = Random.Range(0, availableCharacters.Count);
            ActivateNextCharacter(availableCharacters[randomIndex]);
        }
        else
        {
            Debug.Log("더 이상 소환할 캐릭터가 없습니다.");
        }

        if (currentPlayer != null)
        {
            currentPlayer.SetActive(true);
            currentPlayer.transform.position = new Vector3(0.25f, 2f, 0f);
            currentPlayer.transform.rotation = Quaternion.Euler(-35f, 90f, 0f);
            
            Rigidbody rigid = currentPlayer.GetComponent<Rigidbody>();
            rigid.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    
    public void DeactiveButton()
    {
        Buttons.SetActive(false);
    }

    public void ActiveButton()
    {
        
        Buttons.SetActive(true);
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
