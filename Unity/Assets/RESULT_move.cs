using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class RESULT_move : MonoBehaviour
{
    GameObject result_obj;
    float time;
    SpriteRenderer render;
    bool move_start_flag;
    float sin;
    bool sin_finish = false;

    // Start is called before the first frame update
    void Start()
    {
        result_obj = GameObject.Find("RESULT");
        render = GetComponent<SpriteRenderer>();
        render.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        result_obj.transform.localPosition = new Vector3(0.05f, 3.85f, 0.0f);
        result_obj.transform.localScale = new Vector3(0.9f, 0.9f, 1.0f);
        move_start_flag = false;
        sin_finish = false;
        time = 0.0f;
    }

    public void Reset()
    {
        render.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        result_obj.transform.localPosition = new Vector3(0.05f, 3.85f, 0.0f);
        result_obj.transform.localScale = new Vector3(0.9f, 0.9f, 1.0f);
        move_start_flag = false;
        sin_finish = false;
        time = 0.0f;
    }

    public void Start_motion()
    { 
        if (!move_start_flag)
        {
            time = 0.0f;
            sin_finish = false;
        }
        move_start_flag = true;
    }

    public void display()
    {
        result_obj.transform.localPosition = new Vector3(0.05f, 3.85f, 0.0f);
        result_obj.transform.localScale = new Vector3(0.9f, 0.9f, 1.0f);
        render.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (move_start_flag)
        {
            time += Time.deltaTime * 0.9f;

            render.color = new Color(1.0f, 1.0f, 1.0f, time*2.0f);
            if (time >= 0.5f)
            {
                result_obj.transform.localScale = new Vector3(0.9f, 0.9f, 1.0f);
            }
            else
            {
                result_obj.transform.localScale = new Vector3(1.4f - time, 1.4f - time, 1.0f);
            }

            sin = Mathf.Sin(math.PI * 2.0f * time);
            if (sin < 0.0f)
            {
                sin_finish = true;
                sin = 0.0f;
            }
            if (!sin_finish)
            {
                result_obj.transform.localPosition = new Vector3(0.05f, 3.85f - sin * 0.5f, 0.0f);
            }
            else
            {
                result_obj.transform.localPosition = new Vector3(0.05f, 3.85f, 0.0f);
            }
        }
    }
}
