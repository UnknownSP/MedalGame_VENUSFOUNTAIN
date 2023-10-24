using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OUT_move : MonoBehaviour
{
    GameObject out_obj;
    float time;
    bool start_flag = false;
    bool erase_flag = false;
    SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        out_obj = GameObject.Find("OUT");
        render = GetComponent<SpriteRenderer>();
        render.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        out_obj.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
        out_obj.transform.localScale = new Vector3(0.85f,0.85f,1.0f);
        start_flag = false;
        erase_flag = false;
        time = 0.0f;
    }

    public void Reset()
    {
        render.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        out_obj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        out_obj.transform.localScale = new Vector3(0.85f, 0.85f, 1.0f);
        start_flag = false;
        erase_flag = false;
        time = 0.0f;
    }

    public void Start_indicate()
    {
        if (!start_flag)
        {
            time = 0.0f;
        }
        start_flag = true;
        erase_flag = false;
    }

    public void Start_erase()
    {
        if (!erase_flag)
        {
            time = 0.0f;
        }
        erase_flag = true;
        start_flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (start_flag)
        {
            time += Time.deltaTime;
            render.color = new Color(1.0f, 1.0f, 1.0f, time * 3.0f);
            if (time * 4.0f >= 2.0f)
            {
                out_obj.transform.localScale = new Vector3(0.85f,0.85f,1.0f);
            }
            else
            {

                out_obj.transform.localScale = new Vector3(2.85f- time*4.0f, 2.85f - time * 4.0f, 1.0f);
            }
        }
        if (erase_flag)
        {
            time += Time.deltaTime;
            out_obj.transform.localScale = new Vector3(0.85f, 0.85f, 1.0f);
            render.color = new Color(1.0f,1.0f,1.0f,1.0f- time*1.8f);
        }
    }
}
