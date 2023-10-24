using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Circle_move : MonoBehaviour
{

    [SerializeField] public LineRenderer m_lineRenderer = null; // 円を描画するための LineRenderer
    [SerializeField] private float m_radius = 0;    // 円の半径
    [SerializeField] private float m_lineWidth = 0;    // 円の線の太さ

    private float m_duration = 2; // スケール演出の再生時間（秒）
    private float m_from = 0; // スケール演出の開始値
    private float m_to = 0; // スケール演出の終了値

    private float m_elapedTime;
    private bool end_flag = false;
    private bool half_end_flag = false;
    private bool start_flag = false;
    private bool close_flag = false;
    private bool close_open_flag = false;
    private bool first_call = true;

    private void Start()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        end_flag = false;
        half_end_flag = false;
        start_flag = false;
        close_flag = false;
        close_open_flag = false;
        first_call = true;
        m_elapedTime = 0;
    }

    public void Reset_circle()
    {
        transform.localScale = new Vector3(1, 1, 1);
        half_end_flag = false;
        end_flag = false;
        start_flag = false;
        close_flag = false;
        close_open_flag = false;
        first_call = true;
        m_elapedTime = 0;
    }

    public void CLOSE()
    {
        if (first_call)
        {
            InitLineRenderer();
            m_from = 1;
            m_to = 0.01f;
            start_flag = true;
            close_flag = true;
            first_call = false;
        }
    }

    public void CLOSE_OPEN()
    {
        if (first_call)
        {
            InitLineRenderer();
            m_from = 1;
            m_to = 0.01f;
            start_flag = true;
            close_open_flag = true;
            first_call = false;
        }
    }

    // Start is called before the first frame update
    //private void Start()
    //{
    //    m_lineRenderer = GetComponent<LineRenderer>();
    //    InitLineRenderer();
    //}

    // Update is called once per frame
    private void Update()
    {
        if (start_flag)
        {
            if (close_flag)
            {
                if (!end_flag)
                {
                    m_elapedTime += Time.deltaTime;

                    var amount = m_elapedTime % m_duration / m_duration;
                    var scale = Mathf.Lerp(m_from, m_to, amount);

                    transform.localScale = new Vector3(scale, scale, 1);
                }

                if (m_elapedTime >= m_duration)
                {
                    end_flag = true;
                    transform.localScale = new Vector3(0.01f, 0.01f, 1);
                }
            }
            if (close_open_flag)
            {
                if (!end_flag)
                {
                    m_elapedTime += Time.deltaTime;

                    var amount = m_elapedTime % m_duration / m_duration;
                    var scale = Mathf.Lerp(m_from, m_to, amount);

                    transform.localScale = new Vector3(scale, scale, 1);
                }

                if (m_elapedTime >= m_duration)
                {
                    if (!half_end_flag)
                    {
                        half_end_flag = true;
                        m_from = 0.01f;
                        m_to = 1f;
                        m_elapedTime = 0;
                        transform.localScale = new Vector3(0.01f, 0.01f, 1);
                    }
                    else
                    {
                        end_flag = true;
                        transform.localScale = new Vector3(1f, 1f, 1);
                    }
                }
            }
        }
    }

    public bool Is_end()
    {
        return end_flag;
    }

    public bool Is_half_end()
    {
        return half_end_flag;
    }

    public void InitLineRenderer()
    {
        var segments = 360;

        m_lineRenderer.startWidth = m_lineWidth;
        m_lineRenderer.endWidth = m_lineWidth;
        m_lineRenderer.positionCount = segments;
        m_lineRenderer.loop = true;
        m_lineRenderer.useWorldSpace = false; // transform.localScale を適用するため

        var points = new Vector3[segments];

        for (int i = 0; i < segments; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            var x = Mathf.Sin(rad) * m_radius;
            var y = Mathf.Cos(rad) * m_radius;
            points[i] = new Vector3(x, y, 0);
        }

        m_lineRenderer.SetPositions(points);
        m_elapedTime = 0;
    }
}
