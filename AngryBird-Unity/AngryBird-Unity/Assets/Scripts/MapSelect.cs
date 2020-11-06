using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelect : MonoBehaviour
{
    public int starsNumber; //星星的数量，已经获得完的星星数量，每一个单关星星加在一起的总数
    private bool isSelect = false; //是否已经解锁，当前是否可以点击
    public GameObject locks; //游戏中，星星不够的那把锁
    public GameObject stars; //游戏中，锁掉了以后显示的星星
    public GameObject panel; //对应的哪一个大关卡中的那 12 个小关卡页面
    public GameObject map; //对应的是哪一个大关卡，竖着的那个
    
    private void Start()
    {
        if (PlayerPrefs.GetInt("totalNumber", 0) >= starsNumber)
        { //PlayerPrefs 这里老师讲的是，这是一个键值对，用来存储 星星总数量
            isSelect = true;
        }

        if (isSelect)//符合条件点击
        {
            locks.SetActive(false); //锁头隐藏
            stars.SetActive(true); //星星显示出来
            // map>star>text 的星星数量显示还没写
        }
    }

    public void Selected() //这个针对的是 竖着的那个大关卡
    {
        if (isSelect)//是否符合可以点击的条件
        {
            panel.SetActive(true);
            map.SetActive(false);
        }
    }
}