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
    public GameObject scorePopupPrefab;
    
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
            effectInstance.transform.position = transform.position;
            effectInstance.SetActive(true);
            ParticleSystem particleSystem = effectInstance.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
            }
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
