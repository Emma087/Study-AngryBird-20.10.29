using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;
    public List<Pig> pig;
    public static GameManager _instance;
    private Vector3 firstBirdOriginPosition;
    public GameObject win;
    public GameObject lose;
    public GameObject[] stars;
    private int starsLevelNumber = 0; //12关的每个单关，的星星数量
    private int totalLevelNumber = 12; //每一个地图，对应了 12个单个小关卡

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


    public void ShowStars()
    {
        StartCoroutine("show");
    }

    IEnumerator show() //协程显示星星
    {
        for (starsLevelNumber = 0; starsLevelNumber < birds.Count + 1; starsLevelNumber++)
        {
            Debug.Log(starsLevelNumber);
            if (starsLevelNumber >= stars.Length)
            {
                break;
            }

            yield return new WaitForSeconds(0.2f);
            stars[starsLevelNumber].SetActive(true);
        }

        Debug.Log(starsLevelNumber);
    }

    public void Replay()
    {
        SaveDate();
        SceneManager.LoadScene(2);
    }

    public void HomeMune()
    {
        SaveDate();
        SceneManager.LoadScene(1);
    }

    public void SaveDate() //专门存储 12单关的星星数量的
    {
        if (starsLevelNumber > PlayerPrefs.GetInt(PlayerPrefs.GetString("NowLevel")))
            //如果重玩得到的星星比键值对存的上一次的高，再存，否则就不存
            PlayerPrefs.SetInt(PlayerPrefs.GetString("NowLevel"), starsLevelNumber);
        int sum = 0; //临时的变量，用来存储关卡中所有的星星总数
        for (int i = 1; i <= totalLevelNumber; i++)
        {
            sum += PlayerPrefs.GetInt(PlayerPrefs.GetString("Level" + i.ToString()));
        }

        PlayerPrefs.SetInt("totalNumber", sum); //把 sum 传给键值对数组
    }
}