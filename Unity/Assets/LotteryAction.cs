using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotteryAction : MonoBehaviour
{
    [NonSerialized] public int Value = 0;
    SpriteRenderer Lottery_Render;
    bool _fadeOut = false;
    float time = 0.0f;
    float rotateSpeed = 0.0f;
    float startDegree = 0.0f;
    [SerializeField] public float speedCoeff = 2.0f;
    [SerializeField] public float fadeOutTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Lottery_Render = transform.Find("ROTATE_LOTTERY").gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (_fadeOut)
        {
            this.transform.Rotate(new Vector3(0f, 0f, rotateSpeed * speedCoeff * Time.deltaTime));
            if (time <= fadeOutTime)
            {
                Lottery_Render.color = new Color(1f,1f,1f,(fadeOutTime - time)/fadeOutTime);
            }
            else
            {
                _fadeOut = false;
                rotateSpeed = 0.0f;
                startDegree = 0.0f;
            }
        }
        else
        {
            time = 0.0f;
        }
    }

    public void FadeOut(float Speed, float startDeg)
    {
        this.transform.eulerAngles = new Vector3(0f,0f,startDeg);
        rotateSpeed = Speed;
        startDegree = startDeg;
        _fadeOut = true;
    }
}
