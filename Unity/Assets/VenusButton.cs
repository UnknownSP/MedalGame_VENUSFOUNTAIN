using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VenusButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image buttonImage;

    [NonSerialized] public bool buttonDown = false;
    [NonSerialized] public bool enable = true;

    public static bool pushedButton = false;

    private static readonly Color DISABLE_COLOR = new Color(0.7f, 0.7f, 0.7f);
    private static readonly Color ENABLE_COLOR = new Color(1.0f, 1.0f, 1.0f);

    public void OnButtonDown()
    {
        buttonDown = true;
    }

    public void OnButtonUp()
    {
        buttonDown = false;
    }

    public bool pushed()
    {
        if (pushedButton)
        {
            pushedButton = false;
            return true;
        }
        return false;
    }

    public void onClick()
    {
        pushedButton = true;
    }

    public void EnableButton()
    {
        enable = true;
        button.interactable = true;
        button.enabled = true;
        buttonImage.color = ENABLE_COLOR;
    }

    public void DisableButton()
    {
        enable = false;
        button.interactable = false;
        button.enabled = false;
        buttonImage.color = DISABLE_COLOR;
    }
}
