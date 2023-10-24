using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RESULT_NUM_move : MonoBehaviour
{
    GameObject[] DIGIT = new GameObject[5];
    SpriteRenderer[] RENDER = new SpriteRenderer[5];
    GameObject[] EFFECT_DIGIT = new GameObject[5];
    SpriteRenderer[] EFFECT_RENDER = new SpriteRenderer[5];
    Vector3[] Rotation = new Vector3[5];
    bool start_motion = false;
    bool start_motion_first = true;
    float time;
    float numchange_time;
    int i;
    float scale_over_time;
    float scale_x_y;
    float[] position_x = new float[5];
    float temp;
    int[] broke_digit = new int[5];
    int win_meadl;
    int display_enable_digit_num;
    bool[] display_enable = new bool[5];
    float display_time;
    float[] EFFECT_TIME = new float[5];
    bool start_display = false;
    bool start_display_first = true;

    public Sprite[] NUMBERS = new Sprite[10];
    public Sprite[] EFFECT_NUMBERS = new Sprite[10];

    // Start is called before the first frame update
    void Start()
    {
        for (i=0;i<5;i++)
        {
            DIGIT[i] = GameObject.Find("RESULT_NUM_"+(i+1));
            RENDER[i] = DIGIT[i].GetComponent<SpriteRenderer>();
            RENDER[i].color = new Color(1.0f,1.0f,1.0f,0.0f);
            EFFECT_DIGIT[i] = GameObject.Find("RESULT_EFFECT_NUM_" + (i + 1));
            EFFECT_DIGIT[i].transform.localPosition = new Vector3(3.8f - i * 1.9f, 0.0f, 0.0f);
            EFFECT_DIGIT[i].transform.localScale = new Vector3(0.95f, 0.95f, 1.0f);
            EFFECT_RENDER[i] = EFFECT_DIGIT[i].GetComponent<SpriteRenderer>();
            EFFECT_RENDER[i].color = new Color(1.0f,1.0f,1.0f,0.0f);
            EFFECT_TIME[i] = 0.0f;
            broke_digit[i] = 0;
        }
        time = 0.0f;
        numchange_time = 0.0f;
        display_time = 0.0f;
        start_display = false;
        start_display_first = true;
    }

    public void Reset()
    {
        for (i = 4; i >= 0; i--)
        {
            RENDER[i].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            EFFECT_DIGIT[i].transform.localPosition = new Vector3(3.8f - i * 1.9f,0.0f,0.0f);
            EFFECT_DIGIT[i].transform.localScale = new Vector3(0.95f,0.95f,1.0f);
            EFFECT_RENDER[i].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            display_enable[i] = false;
            EFFECT_TIME[i] = 0.0f;
            broke_digit[i] = 0;
        }
        start_motion = false;
        start_motion_first = true;
        start_display = false;
        start_display_first = true;
        time = 0.0f;
        numchange_time = 0.0f;
        display_time = 0.0f;
        win_meadl = 0;
    }

    public void Start_motion(int win)
    {
        if (!start_motion)
        {
            time = 0.0f;
            numchange_time = 0.0f;
            display_time = 0.0f;
        }
        start_motion = true;
        start_display = false;
        win_meadl = win;
    }

    public void Start_display(int win)
    {
        if (!start_display)
        {
            time = 0.0f;
            numchange_time = 0.0f;
            display_time = 0.0f;
        }
        start_display = true;
        start_motion = false;
        win_meadl = win;
    }

    // Update is called once per frame
    void Update()
    {
        if (start_motion)
        {
            time += Time.deltaTime;
            numchange_time += Time.deltaTime;
            display_time = time - 1.8f;
            if (start_motion_first)
            {
                for (i = 0; i < 5; i++)
                {
                    RENDER[i].sprite = NUMBERS[Random.Range(0, 10)];
                    DIGIT[i].transform.localScale = new Vector3(2.8f, 2.8f, 1.0f);
                    RENDER[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    Rotation[i] = new Vector3(360.0f * Random.value,0.0f,0.0f);
                    broke_digit[i] = Digit_break(win_meadl,i+1);
                }
                for (i = 4; i >= 0; i--)
                {
                    if (broke_digit[i] != 0)
                    {
                        display_enable_digit_num = i + 1;
                        break;
                    }
                    else
                    {
                        display_enable_digit_num = 0;
                    }
                }
                start_motion_first = false;
            }
            if (numchange_time >= 0.08)
            {
                numchange_time -= 0.08f;
                for (i = 0; i < 5; i++)
                {
                    RENDER[i].sprite = NUMBERS[Random.Range(0, 10)];
                }
            }
            scale_over_time = time * 3.46f;
            if (scale_over_time >= 2.08)
            {
                scale_x_y = 0.72f;
                if (!display_enable[0])
                {
                    for (i = 4; i >= 0; i--)
                    {
                        position_x[i] = 3.8f - i * 1.9f;
                    }
                }
            }
            else
            {
                scale_x_y = 2.8f - scale_over_time;
                temp = 1.9f * scale_x_y / 0.72f;
                for (i = 4; i >= 0; i--)
                {
                    position_x[i] = temp * (2.0f - (float)i); 
                }
            }
            for (i=0;i<5;i++)
            {
                if (display_time >= 0.35f*i)
                {
                    display_enable[i] = true;
                }
            }
            if (display_enable[0])
            {
                switch (display_enable_digit_num)
                {
                    case 3:
                        RENDER[2].color = new Color(1.0f,1.0f,1.0f,0.0f);
                        EFFECT_RENDER[0].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                        EFFECT_RENDER[4].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                        if (display_enable[1])
                        {
                            RENDER[3].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                            for (i = 4; i >= 0; i--)
                            {
                                if (i == 0 || i == 1)
                                {
                                    position_x[i] -= Time.deltaTime * 4.75f;
                                }
                                else
                                {
                                    position_x[i] += Time.deltaTime * 4.75f;
                                }
                                Rotation[i].x += 1550.0f * Time.deltaTime;
                            }
                            if (position_x[1] <= 0.0f)
                            {
                                position_x[0] = 1.9f;
                                position_x[1] = 0.0f;
                                position_x[4] = -1.9f;
                            }
                            for (i=0;i<3;i++)
                            {
                                if (display_enable[i+2])
                                {
                                    if (i==2)
                                    {
                                        RENDER[4].sprite = NUMBERS[broke_digit[i]];
                                        Rotation[4].x = 0.0f;
                                    }
                                    else
                                    {
                                        RENDER[i].sprite = NUMBERS[broke_digit[i]];
                                        Rotation[i].x = 0.0f;
                                    }
                                    EFFECT_DIGIT[i + 1].transform.localPosition = new Vector3(1.9f*(1.0f-i),0.0f,0.0f);
                                    EFFECT_RENDER[i + 1].sprite = EFFECT_NUMBERS[broke_digit[i]];
                                    EFFECT_RENDER[i + 1].color = new Color(1.0f,1.0f,1.0f,1.0f-EFFECT_TIME[i+1]*1.5f);
                                    EFFECT_TIME[i + 1] += Time.deltaTime;
                                }
                            }

                        }
                        else if (display_enable[0])
                        {
                            for (i = 4; i >= 0; i--)
                            {
                                if (i==0 || i==1)
                                {
                                    position_x[i] -= Time.deltaTime * 4.75f;
                                }
                                else
                                {
                                    position_x[i] += Time.deltaTime * 4.75f;
                                }
                                Rotation[i].x += 1550.0f * Time.deltaTime;
                            }
                            if (position_x[1] <= 0.95f)
                            {
                                position_x[0] = 0.95f + 1.9f;
                                position_x[1] = 0.95f;
                                position_x[3] = -0.95f;
                                position_x[4] = -0.95f - 1.9f;
                            }
                        }
                        break;
                    case 4:
                        RENDER[2].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                        EFFECT_RENDER[4].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                        if (display_enable[0])
                        {
                            for (i = 4; i >= 0; i--)
                            {
                                if (i == 0 || i == 1)
                                {
                                    position_x[i] -= Time.deltaTime * 4.75f;
                                }
                                else
                                {
                                    position_x[i] += Time.deltaTime * 4.75f;
                                }
                                Rotation[i].x += 1550.0f * Time.deltaTime;
                            }
                            if (position_x[1] <= 0.95f)
                            {
                                position_x[0] = 0.95f + 1.9f;
                                position_x[1] = 0.95f;
                                position_x[3] = -0.95f;
                                position_x[4] = -0.95f - 1.9f;
                            }
                            for (i = 0; i < 4; i++)
                            {
                                if (display_enable[i + 1])
                                {
                                    if (i == 2 || i == 3)
                                    {
                                        RENDER[i+1].sprite = NUMBERS[broke_digit[i]];
                                        Rotation[i+1].x = 0.0f;
                                    }
                                    else
                                    {
                                        RENDER[i].sprite = NUMBERS[broke_digit[i]];
                                        Rotation[i].x = 0.0f;
                                    }
                                    EFFECT_DIGIT[i].transform.localPosition = new Vector3(2.85f - i*1.9f, 0.0f, 0.0f);
                                    EFFECT_RENDER[i].sprite = EFFECT_NUMBERS[broke_digit[i]];
                                    EFFECT_RENDER[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f - EFFECT_TIME[i] * 1.5f);
                                    EFFECT_TIME[i] += Time.deltaTime;
                                }
                            }
                        }
                        break;
                    case 5:
                        for (i = 0; i < 5; i++)
                        {
                            Rotation[i].x += 1550.0f * Time.deltaTime;
                            if (display_enable[i])
                            {
                                RENDER[i].sprite = NUMBERS[broke_digit[i]];
                                Rotation[i].x = 0.0f;
                                EFFECT_DIGIT[i].transform.localPosition = new Vector3(3.8f - i * 1.9f, 0.0f, 0.0f);
                                EFFECT_RENDER[i].sprite = EFFECT_NUMBERS[broke_digit[i]];
                                EFFECT_RENDER[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f - EFFECT_TIME[i] * 1.5f);
                                EFFECT_TIME[i] += Time.deltaTime;
                            }
                        }
                        break;
                }
            }

            for (i = 4; i >= 0; i--)
            {
                if (!display_enable[0])
                {
                    Rotation[i].x += 1550.0f * Time.deltaTime;
                }
                DIGIT[i].transform.localScale = new Vector3(scale_x_y, scale_x_y, 0.0f);
                DIGIT[i].transform.localPosition = new Vector3(position_x[i], 0.0f, 0.0f);
                DIGIT[i].transform.localRotation = Quaternion.Euler(Rotation[i]);
            }

        }
        if (start_display)
        {
            time += Time.deltaTime;
            numchange_time += Time.deltaTime;
            display_time = time - 1.8f;
            if (start_display_first)
            {
                for (i = 0; i < 2; i++)
                {
                    broke_digit[i] = Digit_break(win_meadl, i + 1);
                }
                start_display_first = false;
            }
            if (broke_digit[1] != 0)
            {
                for (i = 0; i < 3; i++)
                {
                    RENDER[i+2].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                }
                for (i=0;i<2;i++)
                {
                    DIGIT[i].transform.localScale = new Vector3(0.72f, 0.72f, 0.0f);
                    DIGIT[i].transform.localPosition = new Vector3(0.95f-i*1.9f, 0.0f, 0.0f);
                    EFFECT_DIGIT[i].transform.localPosition = new Vector3(0.95f - i * 1.9f, 0.0f, 0.0f);
                    RENDER[i].sprite = NUMBERS[broke_digit[i]];
                    EFFECT_RENDER[i].sprite = EFFECT_NUMBERS[broke_digit[i]];
                    RENDER[i].color = new Color(1.0f,1.0f,1.0f,1.0f);
                }
                if (time >= 1.8)
                {
                    for (i = 0; i < 2; i++)
                    {
                        EFFECT_RENDER[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f - display_time * 1.5f);
                    }
                }
                else
                {
                    for (i=0;i<2;i++)
                    {
                        EFFECT_RENDER[i].color = new Color(1.0f,1.0f,1.0f,0.0f);
                    }
                }
            }
            else
            {
                for (i = 0; i < 4; i++)
                {
                    RENDER[i + 1].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                }
                DIGIT[0].transform.localScale = new Vector3(0.72f, 0.72f, 0.0f);
                DIGIT[0].transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                EFFECT_DIGIT[0].transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                RENDER[0].sprite = NUMBERS[broke_digit[0]];
                EFFECT_RENDER[0].sprite = EFFECT_NUMBERS[broke_digit[0]];
                RENDER[0].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                if (time >= 1.8)
                {
                    EFFECT_RENDER[0].color = new Color(1.0f, 1.0f, 1.0f, 1.0f - display_time * 1.5f);
                }
                else
                {
                    EFFECT_RENDER[0].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                }

            }
        }
    }

    private int Digit_break(int data, int digit)
    {
        int return_data;
        int temp;
        int i;

        temp = 1;
        for (i = 0; i < digit; i++)
        {
            temp *= 10;
        }
        return_data = data % (temp);
        temp /= 10;
        return_data /= temp;

        return return_data;
    }
}
