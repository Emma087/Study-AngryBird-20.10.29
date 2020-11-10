using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{
    public int starsNumber; //星星的数量，已经获得完的星星数量，每一个单关星星加在一起的总数
    private bool isSelect = false; //是否已经解锁，当前是否可以点击
    public GameObject locks; //游戏中，星星不够的那把锁
    public GameObject stars; //游戏中，锁掉了以后显示的星星
    public GameObject panel; //对应的哪一个大关卡中的那 12 个小关卡页面
    public GameObject map; //对应的是哪一个大关卡，竖着的那个
    public Text starsText;
    public int startLevelNumber = 1; //开始的关卡，默认是第一关、
    public int endLevelNumber = 12; //第几关结束，虽然只摆了3关，但其实是12关

    private void Start()
    {
        //PlayerPrefs.DeleteAll(); //这个方法能移除掉所有 键值对的信息（就是我们的用户数据）
        if (PlayerPrefs.GetInt("totalNumber", 0) >= starsNumber)
        {
            //PlayerPrefs 这里老师讲的是，这是一个键值对，用来存储 星星总数量
            isSelect = true;
        }

        if (isSelect) //符合条件点击
        {
            locks.SetActive(false); //锁头隐藏
            stars.SetActive(true); //星星显示出来

            // map>star>text 的星星数量显示还没写
            int counts = 0; //这是临时的变量，用来存，12关的所有星星总和
            for (int i = startLevelNumber; i < endLevelNumber; i++)
            {
                counts += PlayerPrefs.GetInt("Level" + i.ToString(), 0);
                //第二章地图的12小关一关都没过，所以默认值是 0
            }

            starsText.text = counts.ToString() + " / 9";
        }
    }

    public void Selected() //这个针对的是 竖着的那个大关卡
    {
        if (isSelect) //是否符合可以点击的条件
        {
            panel.SetActive(true);
            map.SetActive(false);
        }
    }

    public void panelSelect()
    {
        panel.SetActive(false);
        map.SetActive(true);
    }
}