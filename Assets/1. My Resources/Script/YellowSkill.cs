using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowSkill : Skills
{
    private Rigidbody rigid;
    public float forwardSpeed = 40f;
    public float downwardSpeed = 5f;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public override void Activate()
    {
        if (!rigid.isKinematic)
        {
            rigid.velocity = Vector3.down * downwardSpeed + Vector3.right * forwardSpeed;
        }
    }
}
