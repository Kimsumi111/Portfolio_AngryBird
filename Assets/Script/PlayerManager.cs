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

    private Dictionary<Character, Queue<GameObject>> poolDictionary;
    public GameObject redPrefab;
    public GameObject yellowPrefab;
    public GameObject bluePrefab;
    public Canvas WorldUICanvas;
    
    public Trajectory trajectory;
    
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
        InitializePool();
        SpawnCharacter(Character.Red);
    }

    void InitializePool()
    {
        poolDictionary = new Dictionary<Character, Queue<GameObject>>();
        
        poolDictionary.Add(Character.Red, new Queue<GameObject>());
        poolDictionary.Add(Character.Yellow, new Queue<GameObject>());
        poolDictionary.Add(Character.Blue, new Queue<GameObject>());

        PopulatePool(Character.Red, redPrefab, 2);
        PopulatePool(Character.Yellow, yellowPrefab, 1);
        PopulatePool(Character.Blue, bluePrefab, 1);
    }

    void PopulatePool(Character character, GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            poolDictionary[character].Enqueue(obj);
        }
    }

    public GameObject GetObject(Character character)
    {
        if (poolDictionary[character].Count > 0)
        {
            GameObject obj = poolDictionary[character].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject newObj = Instantiate(GetPrefab(character));
            return newObj;
        }
    }

    public void ReturnObject(Character character, GameObject obj)
    {
        obj.SetActive(false);
        poolDictionary[character].Enqueue(obj);
    }

    public void SpawnCharacter(Character characterType)
    {
        GameObject character = GetObject(characterType);
        character.transform.position = new Vector3(0.25f, 2f, 0f);
        character.transform.rotation = Quaternion.Euler(-35f, 90f, 0f);
    }

    GameObject GetPrefab(Character character)
    {
        switch (character)
        {
            case Character.Red:
                return redPrefab;
            case Character.Yellow:
                return yellowPrefab;
            case Character.Blue:
                return bluePrefab;
            default:
                return null;
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
