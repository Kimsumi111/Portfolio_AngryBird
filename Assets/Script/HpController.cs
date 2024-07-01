using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpController : MonoBehaviour
{
    public float maxHp = 100f;
    private float currentHp;
    public GameObject effectPrefab;
    private GameObject effectInstance;
    
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
        Debug.Log(currentHp);
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
}
