using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RedSkill : Skills
{
    public GameObject explosionEffect;
    public float explosionRadius = 5f;
    public float explosionForce = 700f;
    private float damage;

    public override void Activate()
    {
        Explode();
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            HpController hpController = collider.GetComponent<HpController>();
            if (rb != null)
            {
                
                Vector2 explosionDirection = rb.transform.position - transform.position;
                float explosionDistance = explosionDirection.magnitude;

                damage = (explosionForce / explosionDistance);
                rb.AddForce(explosionDirection.normalized * damage, ForceMode.Impulse);
                hpController.TakeDamage(damage);
            }
        }

        Instantiate(explosionEffect, transform.position, Quaternion.identity);
    }
    
    void OnDrawGizmosSelected()
    {
        // 폭발 반경을 시각적으로 표시합니다.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
