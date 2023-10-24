using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
public class ManualToggle : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image backImage;
    [SerializeField] private Image handleImage;
    [SerializeField] private RectTransform handle;
    [SerializeField] private bool onAwake;

    [NonSerialized] public bool Value;

    private float handlePosX;
    private Sequence sequence;

    private static readonly Color OFF_BG_COLOR = new Color(0.92f, 0.92f, 0.92f);
    private static readonly Color ON_BG_COLOR = new Color(0.2f, 0.84f, 0.3f);
    private static readonly Color DISABLE_H_COLOR = new Color(0.7f, 0.7f, 0.7f);
    private static readonly Color ENABLE_H_COLOR = new Color(1.0f, 1.0f, 1.0f);
    private static readonly Color DISABLE_BACK_COLOR = new Color(0.7f, 0.7f, 0.7f);
    private static readonly Color ENABLE_BACK_COLOR = new Color(1.0f, 1.0f, 1.0f);

    private const float SWITCH_DURATION = 0.36f;

    [NonSerialized] public bool enable = true;

    // Start is called before the first frame update
    void Start()
    {
        handlePosX = Mathf.Abs(handle.anchoredPosition.x);
        Value = onAwake;
        UpdateToggle(0);
    }

    public void SwitchToggle()
    {
        if (enable)
        {
            Value = !Value;
            UpdateToggle(SWITCH_DURATION);
        }
    }

    public void EnableToggle()
    {
        enable = true;
        handleImage.color = ENABLE_H_COLOR;
    }

    public void DisableToggle()
    {
        enable = false;
        handleImage.color = DISABLE_H_COLOR;
        if (Value)
        {
            Value = !Value;
            UpdateToggle(SWITCH_DURATION);
        }
    }

    private void UpdateToggle(float duration)
    {
        var bgColor = Value ? ON_BG_COLOR : OFF_BG_COLOR;
        var handleDestX = Value ? handlePosX : -handlePosX;
        if (Value)
        {
            backImage.color = ENABLE_BACK_COLOR;
        }
        else
        {
            backImage.color = DISABLE_BACK_COLOR;
        }

        sequence?.Complete();
        sequence = DOTween.Sequence();
        sequence.Append(backgroundImage.DOColor(bgColor, duration))
            .Join(handle.DOAnchorPosX(handleDestX, duration / 2));
    }
}
