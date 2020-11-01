using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    private SpriteRenderer render; //声明一个 精灵渲染器 的变量，可以用于修改图片资源

    public Sprite hurtImage;

    // public Sprite deathImage;
    public GameObject boom;
    public GameObject score;

    public bool isPig; //要么就是猪，要不是就是木头块
    

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude > maxSpeed) //如果相对速度大于最大的速度，猪死
        {
            Dead();
        }
        else if (other.relativeVelocity.magnitude > minSpeed && other.relativeVelocity.magnitude < maxSpeed)
            //如果相对速度大于最小速度，又小于最大速度，那么猪受伤
        {
            render.sprite = hurtImage;
        }
    }

    void Dead()
    {
        if (isPig)
        {
            GameManager._instance.pig.Remove(this);
        }

        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);

        GameObject go = Instantiate(score, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        Destroy(go, 1.5f);
    }


}