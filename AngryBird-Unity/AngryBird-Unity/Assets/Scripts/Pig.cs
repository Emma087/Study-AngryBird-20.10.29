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
    public AudioClip pigHurtMusic; //猪猪手上时候的音效
    public AudioClip pigDeadMusic; //猪猪爆炸时候的音效
    public AudioClip birdCollisionMusic; //鸟撞上猪的音效

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioPlay(birdCollisionMusic);
            other.transform.GetComponent<Bird>().Hurt();
        }

        if (other.relativeVelocity.magnitude > maxSpeed) //如果相对速度大于最大的速度，猪死
        {
            Dead();
        }
        else if (other.relativeVelocity.magnitude > minSpeed && other.relativeVelocity.magnitude < maxSpeed)
            //如果相对速度大于最小速度，又小于最大速度，那么猪受伤
        {
            render.sprite = hurtImage;
            AudioPlay(pigHurtMusic);
        }
    }

     public void Dead()
    {
        if (isPig)
        {
            GameManager._instance.pig.Remove(this);
        }

        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        AudioPlay(pigDeadMusic);

        GameObject go = Instantiate(score, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        Destroy(go, 1.5f);
    }

    public void AudioPlay(AudioClip clip)
    {
        if (!isPig)
        {
            return;
        }
        // // 报错了就debug.log.  先看对象是不是空。  再打印是哪个 gameobject 报错的。
        // Debug.Log( gameObject.name + (clip == null).ToString());
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}