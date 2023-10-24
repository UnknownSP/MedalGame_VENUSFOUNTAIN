using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class touchRotate : MonoBehaviour
{

    [SerializeField] public float roundTripTime = 0.5f;
    float time = 0.0f;
    bool _start = false;
    const float minScale = 1.34f;
    const float maxScale = 1.40f;
    float scale = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (_start)
        {
            if (time < roundTripTime)
            {
                scale = maxScale - (maxScale - minScale) * time * 2.0f;
                this.transform.localScale = new Vector3(scale,scale,1f);
            }
            else if(time < roundTripTime * 2.0f)
            {
                scale = minScale + (maxScale - minScale) * (time - roundTripTime) * 2.0f;
                this.transform.localScale = new Vector3(scale, scale, 1f);
            }
            else
            {
                this.transform.localScale = new Vector3(maxScale, maxScale, 1f);
                time = 0.0f;
            }
        }
        else
        {
            time = 0.0f;
        }
    }

    public void startAnimation()
    {
        _start = true;
        this.transform.localScale = new Vector3(maxScale, maxScale, 1f);
    }
    public void stopAnimation()
    {
        _start = false;
        this.transform.localScale = new Vector3(maxScale, maxScale, 1f);
    }
}
