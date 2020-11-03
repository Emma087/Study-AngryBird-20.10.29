using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;
    public List<Pig> pig;
    public static GameManager _instance;
    private Vector3 firstBirdOriginPosition;
    public GameObject win;
    public GameObject lose;

    private void Awake()
    {
        _instance = this;
        if (birds.Count > 0)
        {
            firstBirdOriginPosition = birds[0].transform.position;
        }
    }

    private void Start()
    {
        Initialized();
    }

    /// <summary>
    /// 初始化小鸟（Initialize 初始化的意思）
    /// </summary>
    private void Initialized()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            if (i == 0) //第一只小鸟
            {
                birds[0].transform.position = firstBirdOriginPosition;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }

    public void ShowStars()
    {
        
    }

    /// <summary>
    /// 判定游戏是否胜出的逻辑
    /// </summary>
    public void NextBird()
    {
        if (pig.Count > 0)
        {
            if (birds.Count > 0)
            {
                //下一只准备
                Initialized();
            }
            else
            {
                //输了，播放输了动画
                lose.SetActive(true);
            }
        }
        else
        {
            //赢了，播放赢了动画
            win.SetActive(true);
        }
    }
}