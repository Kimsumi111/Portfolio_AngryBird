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
using UnityEngine.SceneManagement;
using DG.Tweening;

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

    public Button redButton;
    public Button yellowButton;
    public Button blueButton;

    public Image redImage;
    public Image yellowImage;
    public Image blueImage;
    
    public Trajectory trajectory;
    public FollowCamera followCamera;
    public int redCount = 2;
    public int yellowCount = 1;
    public int blueCount = 1;

    public ScaleStars scaleStars;
    public GameObject stageEndCanvas;
    public GameObject skillBtn;
    public Button skillButton;
    private Color skilloriginColor;
    private Tween skillButtonTween;
    
    private List<GameObject> redList;
    private List<GameObject> yellowList;
    private List<GameObject> blueList;

    private GameObject currentPlayer;
    private Character currentCharacter;

    private Dictionary<Character, int> characterUses;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 원래 싱글톤은 이렇게 안쓰지만
            // 기존 오브젝트라도 지워줘야
            // Instance의 this가 안꼬여서요
            
            // Trajectory 스크립트를 두 씬에서 같이 써서 생긴 문제였던 건가요?
            // 꼭 그렇기보단 PlayerManager가 안사라지는 오브젝트인데
            // 거기에 물린 무수히 많은 링크들이 작동을 하려고 해서 에러가 많았던것
       
            // 그러면 애초에 싱글톤으로 하지 말아야 했던 건가요?
            // DontDestroy를 하면 안됐던것 같은데요.
            
            DestroyImmediate(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(Instance.gameObject);
        }
    }

    protected virtual void Start()
    {
        InitializePlayers();
        InitializeCharacterUses();
        UpdateCharacterUsesText();
        
        skillButton.onClick.AddListener(SkillEvent.SkillButtonClicked);
    }

    protected void InitializePlayers()
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
    
    private Transform newCharacterTransform;
    protected void UpdateCharacterUsesText()
    {
        redText.text = (characterUses[Character.Red]).ToString();
        yellowText.text = (characterUses[Character.Yellow]).ToString();
        blueText.text = (characterUses[Character.Blue]).ToString();
        
        redButton.interactable = characterUses[Character.Red] > 0;
        yellowButton.interactable = characterUses[Character.Yellow] > 0;
        blueButton.interactable = characterUses[Character.Blue] > 0;
        
        if (redButton.interactable == false)
            SetAlpha(redImage, 0.7f);
        if (yellowButton.interactable == false)
            SetAlpha(yellowImage, 0.7f);
        if (blueButton.interactable == false)
            SetAlpha(blueImage, 0.7f);
    }

    void SetAlpha(Image image, float alpha)
    {
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }

    public event Action<Transform> OnCharacterChanged; 
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
            
            OnCharacterChanged?.Invoke(currentPlayer.transform);
            CameraManager.Instance.SetNewPlayeranim(currentPlayer.GetComponent<Animator>());
        }

        else
        {
            Debug.Log("더 이상 활성화할 캐릭터가 없습니다");
        }
    }

    void ActivateFailCanvas()
    {
        skillBtn.SetActive(false);
        stageEndCanvas.SetActive(true);
        scaleStars.SetFailStarImage();
    }
    
    public void DeactivateCurrentCharacter()
    {
        if (currentPlayer != null)
        {
            currentPlayer.SetActive(false);
            currentPlayer = null;
            followCamera.currentTarget = null;
        }
        
        skillBtn.SetActive(false);
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
            ActivateFailCanvas();
        }

        if (currentPlayer != null)
        {
            currentPlayer.SetActive(true);
            currentPlayer.transform.position = new Vector3(0.25f, 2f, 0f);
            currentPlayer.transform.rotation = Quaternion.Euler(-35f, 90f, 0f);
            
            Rigidbody rigid = currentPlayer.GetComponent<Rigidbody>();
            rigid.constraints = RigidbodyConstraints.FreezeAll;
            
            OnCharacterChanged?.Invoke(currentPlayer.transform);
            
            CameraManager.Instance.SetNewPlayeranim(currentPlayer.GetComponent<Animator>());
        }
    }
    
    public void DeactiveButton()
    {
        Buttons.SetActive(false);
        skillBtn.SetActive(true);
        skillButtonTween = skillButton.transform.DOScale(Vector3.one * 1.2f, 0.6f).SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void ActiveButton()
    {
        Buttons.SetActive(true);
        skillBtn.SetActive(false);
    }
    
    public void SkillBtnClicked()
    {
        skillButton.image.color = Color.gray;
        skillButton.transform.localScale = Vector3.one * 0.95f;
    }

    public void SkillBtnExited()
    {
        skillButton.image.color = Color.white;
        if (skillButtonTween != null)
        {
            skillButtonTween.Kill();
            skillButton.transform.localScale = Vector3.one;
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
