using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowSlider : MonoBehaviour
{
    [SerializeField] private Text speedText;
    [SerializeField] private Slider slider;
    [NonSerialized] public int Value = 0;
    // Start is called before the first frame update
    void Start()
    {
        speedText.text = ((int)slider.value).ToString();
        Value = ((int)slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableSlider()
    {
        slider.enabled = true;
        slider.interactable = true;
    }
    public void DisableSlider()
    {
        slider.enabled = false;
        slider.interactable = false;
    }

    public void valueChange()
    {
        speedText.text = ((int)slider.value).ToString();
        Value = ((int)slider.value);
    }
}
