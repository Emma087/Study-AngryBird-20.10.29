using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Bird : MonoBehaviour
{
    private bool isClick = false;
    public Transform springOriginPosition; //弹簧固定头的位置所在
    public float maxDistance; //弹簧能拉动的最大的距离


    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnMouseDown() //鼠标按下
    {
        isClick = true;
    }

    private void OnMouseUp() //鼠标抬起
    {
        isClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(transform.position, springOriginPosition.position) > maxDistance)
            {
                Vector3 pos = (transform.position - springOriginPosition.position).normalized;
                // 由原点向外的坐标值，减去靠近原点的坐标值，能得出来一个向量，而 normalized，能归一向量，其实就是把向量改成 1
                pos *= maxDistance;
                transform.position = pos + springOriginPosition.position;
            }
        }
    }
}