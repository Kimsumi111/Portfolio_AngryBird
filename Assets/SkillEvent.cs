using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEvent : MonoBehaviour
{
    public static event Action OnSkillButtonClicked;

    public static void SkillButtonClicked()
    {
        OnSkillButtonClicked?.Invoke();
    }
}
