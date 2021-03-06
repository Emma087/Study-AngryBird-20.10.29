﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;


public class Bird : MonoBehaviour
{
    private bool isClick = false;
    public Transform springOriginPosition; //弹簧固定头的位置所在，同时也是右端画线开始的位置
    public float maxDistance; //弹簧能拉动的最大的距离

    [HideInInspector] // 加上这个 sp 虽然是 public 修饰，但是 Inspector窗口不显示
    public SpringJoint2D sp;

    protected Rigidbody2D rg; //受保护类型，子类能看见，引擎看不见

    public LineRenderer lineRight;
    public LineRenderer lineLeft;
    public Transform printLeftLineOriginPosition; //左端画线开始的位置
    private Vector3 birdLengh;
    private Collider2D birdCollider;

    public GameObject boomBird;
    protected Trail myTrail;
    [HideInInspector]
    public bool canMove = false; //限制每一只鸟，一上来就可以被点击的状态
    public float smoothLerpValue;
    public AudioClip selectBirdClickMusic;
    public AudioClip birdFlyingMusic;
    private bool isFly = false; //判定是否已经飞起来的布尔值
    [HideInInspector]
    public bool isReLeaved = false; //是否松开了鼠标，是否释放了小鸟
    
    public Sprite hurt; // 小鸟受伤图片
    protected SpriteRenderer render;
    
    
    private void Awake() //延迟运行的代码
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        //birdLengh = GetComponent(CircleCollider2D).position
        birdCollider = GetComponent<Collider2D>();
        birdLengh = birdCollider.bounds.size; //获得的是 Collider 的尺寸，应该是指直径
        myTrail = GetComponent<Trail>();
        render = GetComponent<SpriteRenderer>();
    }
    

    private void OnMouseDown() //鼠标按下，鼠标正在选择中小鸟，正在点击着那一只鸟
    {
        if (canMove)
        {
            AudioPlay(selectBirdClickMusic);
            isClick = true;
            rg.isKinematic = true;
        }
    }

    private void OnMouseUp() //鼠标抬起
    {
        if (canMove)
        {
            isClick = false;
            rg.isKinematic = false;
            Invoke("Fly", 0.1f);
            lineRight.enabled = false; //鼠标抬起的时候，画线组件失活
            lineLeft.enabled = false;
            canMove = false;
        }
    }

    void Fly() //让小鸟的弹簧组件失效，实现飞出去的运动状态
    {
        isReLeaved = true;
        isFly = true;
        AudioPlay(birdFlyingMusic);
        myTrail.TrailsStart();
        sp.enabled = false;
        Invoke("NextBirdReadyAndCurrentBirdDeath", 2);//小鸟几秒以后删除
    }

    void PrintLine()
    {
        lineRight.enabled = true;
        lineLeft.enabled = true;

        lineRight.SetPosition(0, springOriginPosition.position); //画线第一个点
        lineRight.SetPosition(1, transform.position - birdLengh / 3); //画线第二个点
        lineLeft.SetPosition(0, printLeftLineOriginPosition.position);
        lineLeft.SetPosition(1, transform.position - birdLengh / 3);
    }


    /// <summary>
    /// 当前小鸟，在 List中删除，自身对象删除，产生一个爆炸特效
    /// </summary>
    protected virtual void NextBirdReadyAndCurrentBirdDeath()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boomBird, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) //实时监测是否点击了 UI界面
        {
            return; //如果点击了UI界面，返回出这个方法，以下的内容不执行了
        }

        if (isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //transform.position = transform.position + new Vector3(0, 0, 20);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            if (Vector3.Distance(transform.position, springOriginPosition.position) > maxDistance)
            {
                Vector3 pos = (transform.position - springOriginPosition.position).normalized;
                // 由原点向外的坐标值，减去靠近原点的坐标值，能得出来一个向量，而 normalized，能归一向量，其实就是把向量改成 1
                pos *= maxDistance;
                transform.position = pos + springOriginPosition.position;
            }

            PrintLine();
        }

        //相机跟随
        float birdPositionX = transform.position.x;
        Vector3 limitCameraPosition =
            new Vector3(Mathf.Clamp(birdPositionX, 0, 15), Camera.main.transform.position.y,
                Camera.main.transform.position.z);
        Camera.main.transform.position =
            Vector3.Lerp(Camera.main.transform.position, limitCameraPosition,
                smoothLerpValue * Time.deltaTime); //差值运算，得出一个平滑的效果

        if (isFly)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowSkillYellowBirdAccelerate();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isFly = false;
        myTrail.TrailsClear();
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position); 
        //这个函数，在世界空间指定的位置播放 AutioClip，参数 音频信息，位置，注意这里的位置写的是物体的位置，因为是 2D游戏
    }

    /// <summary>
    /// 小黄鸟专属，在空中加速度
    /// </summary>
    protected virtual void ShowSkillYellowBirdAccelerate()
    {
        isFly = false;
    }

    public void Hurt()
    {
        render.sprite = hurt;
    }
}