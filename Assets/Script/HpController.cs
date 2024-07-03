using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HpController : MonoBehaviour
{
    public float maxHp = 100f;
    private float currentHp;
    public GameObject effectPrefab;
    private GameObject effectInstance;
    public int scoreValue;
    
    void Start()
    {
        currentHp = maxHp;

        if (effectPrefab != null)
        {
            effectInstance = Instantiate(effectPrefab);
            effectInstance.SetActive(false);
        }
    }


    public void TakeDamage(float damage)
    {
        currentHp -= damage;
 
        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (effectInstance != null)
        {
            effectInstance.transform.SetParent(PlayerManager.Instance.WorldUICanvas.transform);
            effectInstance.transform.position = transform.position;
            effectInstance.SetActive(true);
        }
        Destroy(this.gameObject);
    }
    
    private void OnDestroy()
    {
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(scoreValue);
        }
    }
}
