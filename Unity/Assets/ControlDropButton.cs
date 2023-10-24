using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlDropButton : MonoBehaviour
{
    [SerializeField] private Dropdown button;
    [SerializeField] private Image buttonImage;

    [NonSerialized] public int selected = 0;
    [NonSerialized] public bool enable = true;


    private static readonly Color DISABLE_COLOR = new Color(0.7f, 0.7f, 0.7f);
    private static readonly Color ENABLE_COLOR = new Color(1.0f, 1.0f, 1.0f);

    public void valueChange()
    {
        selected = button.value;
    }

    public void EnableDropDown()
    {
        enable = true;
        button.interactable = true;
        button.enabled = true;
        buttonImage.color = ENABLE_COLOR;
    }

    public void DisableDropDown()
    {
        enable = false;
        button.interactable = false;
        button.enabled = false;
        buttonImage.color = DISABLE_COLOR;
    }
}
