using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_touka_move : MonoBehaviour
{
    float time;
    bool start_flag = false;
    SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        render.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        start_flag = false;
        time = 0.0f;
    }

    public void Reset()
    {
        render.color = new Color(1.0f,1.0f,1.0f,0.0f);
        start_flag = false;
        time = 0.0f;
    }

    public void Start_motion()
    {
        if (!start_flag)
        {
            time = 0.0f;
        }
        start_flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (start_flag)
        {
            time += Time.deltaTime;
            render.color = new Color(1.0f, 1.0f, 1.0f, time * 2.1f);
        }
    }
}
