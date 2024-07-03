using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject effectPrefab;
    private GameObject effectInstance;
    
    private void Start()
    {
        StartCoroutine(WaitForEffect());
    }
    
    public float damageMultiplier = 1f;
    private float impactForce = 0f;
    private void OnCollisionEnter(Collision other)
    {
        // 충돌 시 이펙트
        if (!other.gameObject.CompareTag("Terrain"))
        {
            if (effectInstance != null)
            {
                effectInstance.transform.position = other.contacts[0].point;
                effectInstance.SetActive(true);
                ParticleSystem particleSystem = effectInstance.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    particleSystem.Play();
                }
            }
            // hp 깎기
            impactForce = other.relativeVelocity.magnitude * other.rigidbody.mass;
            HpController hp = other.gameObject.GetComponent<HpController>();
            if (hp != null)
            {
                float damage = impactForce * damageMultiplier;
                hp.TakeDamage(damage);
            }
        }
    }

    IEnumerator WaitForEffect()
    {
        yield return new WaitForSeconds(8f);
        if (effectPrefab != null)
        {
            effectInstance = Instantiate(effectPrefab);
            effectInstance.SetActive(false);
        }
    }
}
