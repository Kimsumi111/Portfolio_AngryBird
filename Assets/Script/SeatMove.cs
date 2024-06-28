using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class SeatMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigid;
    
    private Transform playerTrns;
    
    private Vector3 initPosition;
    private Quaternion initRotation;
    private Vector3 targetPosition;
    
    private bool isFollowing = false;
    private Vector3 offset;

    [SerializeField]
    private GameObject seatAnchor;

    private SeatAnchorMove seatAnchorMove;
    
    private void Awake()
    {
        seatAnchorMove = seatAnchor.GetComponent<SeatAnchorMove>();
    }

    private void Start()
    {
        if (seatAnchorMove == null)
        {
            Debug.LogError("seatAnchorMove를 찾을 수 없음");
            return;
        }
        
        initPosition = this.transform.position;
        initRotation = this.transform.rotation;
        rigid.constraints = RigidbodyConstraints.FreezeAll;
        
        rigid.useGravity = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rigid.constraints = RigidbodyConstraints.FreezeRotation;
            
            isFollowing = true;
            playerTrns = seatAnchor.transform;

            Debug.Log(playerTrns.position);
            transform.position = playerTrns.position;
        }

        if (Input.GetMouseButtonUp(0))
        {
            rigid.useGravity = true;
            rigid.constraints = RigidbodyConstraints.None;

            StartCoroutine(Return());
        }
    }
    
    IEnumerator Return()
    {
        yield return new WaitForSeconds(0.5f);
        isFollowing = false;
        transform.DOMove(initPosition, 0.2f);
        
        yield return new WaitForSeconds(0.2f);
        
        // 초기 위치로 돌아감
        transform.position = initPosition;
        transform.rotation = initRotation;
        rigid.constraints = RigidbodyConstraints.FreezeAll;
        rigid.velocity = Vector3.zero;
        rigid.useGravity = false;

        yield return null;
    }
}
