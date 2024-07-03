using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParticleObject : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private TextMeshProUGUI text;
    private RectTransform textRectTransform;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        textRectTransform = text.GetComponent<RectTransform>();
        
        yield return new WaitForSeconds(2.0f);
        
        if (particleSystem)
            particleSystem.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (text)
        {
            text.alpha -= Time.deltaTime;
            textRectTransform.sizeDelta += Vector2.up * Time.deltaTime;
        }
    }
}
