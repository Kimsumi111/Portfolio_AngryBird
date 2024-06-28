using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SeatAnchorMove : MonoBehaviour
{
    public Rigidbody rigid;

    // 최대 게이지 파워
    public float MaxPower = 0f;
    public bool bSnapped = false;
    
    private Vector3 clickMousePosition = Vector3.zero;
    public float MaxGapSize = 0.0f;
    
    public Vector3 initPosition;
    private Quaternion initRotation;
    
    private float elapsedTime = 0.0f;
    public bool isThrown = false;

    private GameManager gameManager;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rigid.useGravity = false;
        isThrown = false;
        
        // 물리 끝난 후 다음 차례가 되면 돌아오기 위한 포지션
        initPosition = transform.position;
        initRotation = transform.rotation;
        rigid.constraints = RigidbodyConstraints.FreezeAll;
    }

    void Shot(Vector3 dir, float normalized)
    {
        // 방향 계산해서 그쪽으로 날린다
        rigid.velocity = dir * (normalized * MaxPower);
        // 땡기기 모드 종료
        bSnapped = false;
        isThrown = true;
        StartCoroutine(Return());
    }
    
    private void Update()
    {
        // 왼쪽 마우스 클릭 되면 (1은 오른쪽)
        if (Input.GetMouseButtonDown(0))
        {
            isThrown = false;
            rigid.useGravity = true;
            // transform.rotation = Quaternion.identity;
            rigid.constraints = RigidbodyConstraints.None;
            
            // 현재 클릭했던 마우스 위치 기억
            clickMousePosition = Input.mousePosition;
            // 땡기기 모드 들어감
            bSnapped = true;
        }
        
        // 땡기기 모드일때
        if (bSnapped)
        {
            // 현재 마우스 좌표를 월드 좌표로 변환
            Vector3 currentMousePosition = GetMouseWorldPosition();
            
            // 처음 마우스 찍었던 좌표에서 현재 마우스 좌표를 빼면 앵그리버드가 날아가야 할 방향이 뜬다.
            Vector3 gap = clickMousePosition - Input.mousePosition;
            float currentGap = gap.magnitude / 50.0f;
            
            // 그 길이가 마우스 최대 허용범위를 넘어서면
            if (currentGap >= MaxGapSize)
            {
                // 마우스 최대 허용 범위로 바뀐다.
                currentGap = MaxGapSize;
            }
            
            // 원래 자리에서 갭을 뺌. 플레이어 위치 업데이트
            // 원래 자리에서 일정 비율로 갭을 빼서 플레이어 위치 업데이트
            Vector3 newPosition = initPosition - (gap.normalized * currentGap);
            transform.position = newPosition;
            
            // gap에 방향이 있다면 -> 옆으로 처다보니까 forward에다가 dir을 넣어준다.
            Vector3 dir = new Vector3(gap.x, gap.y, 0).normalized;
            if (dir != Vector3.zero)
                transform.forward = dir;
            
            // 땡기기 모드였다가 마우스를 땠을때
            if (Input.GetMouseButtonUp(0))
            {
                // 앵그리 버드를 날린다.
                Shot(dir, (currentGap * 30.0f) / MaxGapSize);
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Adjust z-position for the camera's near clip plane
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    
    IEnumerator Return()
    {
        yield return new WaitForSeconds(0.5f);
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
