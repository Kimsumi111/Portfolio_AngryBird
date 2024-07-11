using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HpController : MonoBehaviour
{
    public float maxHp = 100f;
    protected float currentHp;
    public GameObject effectPrefab;
    private GameObject effectInstance;
    public int scoreValue;
    
    
    protected virtual void Start()
    {
        currentHp = maxHp;

        if (effectPrefab != null)
        {
            effectInstance = Instantiate(effectPrefab);
            effectInstance.SetActive(false);
        }

        if (this.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.RegisterEnemy(this);
        }
    }


    public virtual void TakeDamage(float damage)
    {
        currentHp -= damage;
 
        if (currentHp <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        if (effectInstance != null)
        {
            effectInstance.transform.SetParent(PlayerManager.Instance.WorldUICanvas.transform);
            effectInstance.transform.position = transform.position;
            effectInstance.SetActive(true);
        }
        Destroy(this.gameObject);
    }
    
    protected void OnDestroy()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }
        
        if (this.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.UnregisterEnemy(this);
        }
    }
}
