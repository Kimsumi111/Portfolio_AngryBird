using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossHpBar : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI hpText;
    
    [SerializeField] private RectTransform maskTrasform;
    [SerializeField] private RectTransform backgroundTransform;

    private float maxWidth;
    private float maxHeight;

    public BossHpController _script;

    void Awake()
    {
        maxWidth = backgroundTransform.sizeDelta.x;
        maxHeight = backgroundTransform.sizeDelta.y;

        _script.HpStatusBroadCastDelegates += UpdateHpStatus;
        
    }

    public void UpdateHpStatus(float currentHp, float maxHp)
    {
        currentHp = Mathf.Ceil(currentHp);
        hpText.text = $"{currentHp} / {maxHp}";

   
        float factor = 1.0f;
        if (maxHp != 0.0f)
        {
            factor = currentHp / maxHp;
        }
 
        maskTrasform.sizeDelta = new Vector2(factor * maxWidth, maxHeight);
    }
}
