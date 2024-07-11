using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroyPortal
{
    void DestroyPortal();
}

public class BlueSkill : Skills, IDestroyPortal
{
    public GameObject portalEffect;
    public GameObject missileEffect;
    
    private Vector3 _currentPosition;
    
    [NonSerialized]
    public GameObject portal1 = null;
    [NonSerialized]
    public GameObject portal2 = null;
    [NonSerialized]
    public GameObject portal3 = null;
    [NonSerialized]
    public GameObject portal4 = null;
    [NonSerialized]
    public GameObject portal5 = null;
    
    public override void Activate()
    {
        _currentPosition = transform.position;
        
        portal1 = CreateMissileReturnPortal(Vector3.up * 2.0f);
        portal2 = CreateMissileReturnPortal(Vector3.forward * 2.0f);
        portal3 = CreateMissileReturnPortal(Vector3.back * 2.0f);
        portal4 = CreateMissileReturnPortal(Vector3.down * 2.0f + Vector3.forward * 4.0f);
        portal5 = CreateMissileReturnPortal(Vector3.down * 2.0f + Vector3.back * 4.0f);
    }

    GameObject CreateMissileReturnPortal(Vector3 vector3)
    {
        Instantiate(missileEffect, _currentPosition + vector3,
            Quaternion.Euler(90f, 90f, 0f));
        return Instantiate(portalEffect, _currentPosition + vector3,
            Quaternion.Euler(90f, 90f, 0f));
    }

    public void DestroyPortal()
    {
        if (portal1 != null) Destroy(portal1);
        if (portal2 != null) Destroy(portal2);
        if (portal3 != null) Destroy(portal3);
        if (portal4 != null) Destroy(portal4);
        if (portal5 != null) Destroy(portal5);
    }
}
