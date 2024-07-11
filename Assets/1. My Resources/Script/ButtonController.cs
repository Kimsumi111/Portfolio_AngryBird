using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using Unity.VisualScripting;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleMualtiplier = 1.2f;
    public float animationDuration = 0.05f;

    public Character characterType;
    
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(originalScale * scaleMualtiplier, animationDuration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(originalScale, animationDuration);
    }

    public void OnButtonClick()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.ActivateNextCharacter(characterType);
        }
    }
}
