using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomETFXFireProjectile : MonoBehaviour
{
    [SerializeField]
    public GameObject projectilePrefab;  // 하나의 미사일 프리팹만 사용
    [Header("Missile spawns at attached game object")]
    public Transform spawnPosition;
    public float speed = 500;

    void Start()
    {
        FireProjectile(); // 게임 시작 시 미사일 발사
    }

    private void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition.position, Quaternion.identity) as GameObject; // 미사일 생성
        projectile.transform.up = spawnPosition.up; // 미사일 방향 설정
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed); // 미사일에 힘 적용
    }
}
