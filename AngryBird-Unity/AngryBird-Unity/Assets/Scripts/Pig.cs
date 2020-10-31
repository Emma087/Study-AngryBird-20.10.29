using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    private SpriteRenderer render;//声明一个 精灵渲染器 的变量，可以用于修改图片资源
    public Sprite hurtImage;
    // public Sprite deathImage;
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude > maxSpeed) //如果相对速度大于最大的速度，猪死
        {
            Destroy(gameObject);
        }
        else if (other.relativeVelocity.magnitude > minSpeed && other.relativeVelocity.magnitude < maxSpeed)
            //如果相对速度大于最小速度，又小于最大速度，那么猪受伤
        {
            render.sprite = hurtImage;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
    }
}