using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Bird : MonoBehaviour
{
    private bool isClick = false;
    public Transform springOriginPosition; //弹簧固定头的位置所在，同时也是右端画线开始的位置
    public float maxDistance; //弹簧能拉动的最大的距离

    [HideInInspector] // 加上这个 sp 虽然是 public 修饰，但是 Inspector窗口不显示
    public SpringJoint2D sp;

    private Rigidbody2D rg;

    public LineRenderer lineRight;
    public LineRenderer lineLeft;
    public Transform printLeftLineOriginPosition; //左端画线开始的位置
    private Vector3 birdLengh;
    private Collider2D birdCollider;

    public GameObject boomBird;
    private Trail myTrail;

    private void Awake() //延迟运行的代码
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        //birdLengh = GetComponent(CircleCollider2D).position
        birdCollider = GetComponent<Collider2D>();
        birdLengh = birdCollider.bounds.size; //获得的是 Collider 的尺寸，应该是指直径

        myTrail = GetComponent<Trail>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnMouseDown() //鼠标按下
    {
        isClick = true;
        rg.isKinematic = true;
    }

    private void OnMouseUp() //鼠标抬起
    {
        isClick = false;
        rg.isKinematic = false;
        Invoke("Fly", 0.1f);
        lineRight.enabled = false; //鼠标抬起的时候，画线组件失活
        lineLeft.enabled = false;
    }

    void Fly() //让小鸟的弹簧组件失效，实现飞出去的运动状态
    {
        myTrail.TrailsStart();
        sp.enabled = false;
        Invoke("CurrentBirdDeath", 5);
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
    void CurrentBirdDeath()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boomBird, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        myTrail.TrailsClear();
    }
}