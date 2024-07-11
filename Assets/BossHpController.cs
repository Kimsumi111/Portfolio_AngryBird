using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HpStatusBroadCast(float currentHp, float maxHp);
public class BossHpController : HpController
{
    public HpStatusBroadCast HpStatusBroadCastDelegates;
    
    protected override void Start()
    {
        base.Start();
        MaxHp = maxHp;
        CurrentHp = currentHp;
    }

    public override void TakeDamage(float damage)
    {
        if (CurrentHp > 0)
        {
            CurrentHp -= damage;
            if (CurrentHp <= 0)
            {
                Die();
            }
        }
    }
    
    private float CurrentHp
    {
        get => currentHp;
        set
        {
            if (currentHp <= 0)
                return;
            
            currentHp = Mathf.Max(0, value);  // 0보다 이하는 방지하기 위해 둘 중 큰 값 보냄.
            HpStatusBroadCastDelegates?.Invoke(CurrentHp, MaxHp);
        }
    }

    [field: SerializeField]
    private float MaxHp
    {
        get => maxHp;
        set
        {
            maxHp = value;
            HpStatusBroadCastDelegates?.Invoke(CurrentHp, MaxHp);
        }
    }
}
