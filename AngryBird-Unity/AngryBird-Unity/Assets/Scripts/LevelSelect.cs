using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public bool isSelect = false; //是否已经解锁，当前是否可以点击
    public Sprite levelBG; //可以点击以后的，切换的那个不带锁的图片
    private Image image;
    public GameObject[] stars; //拿到每个单关的星星数量，的那个数组

    private void Awake()
    {
        image = GetComponent<Image>(); //这个能获得引擎中的那张图片资源
    }

    private void Start()
    {
        if (transform.parent.GetChild(0).name == gameObject.name)
        {
            isSelect = true;
        }
        else
        {   // 判断当前的关卡是否可以选择，基于前一个关卡，如果
            int beforeLevelNumber = int.Parse(gameObject.name) - 1; //拿到前一个关卡所对应的数字
            if (PlayerPrefs.GetInt("Level" + beforeLevelNumber.ToString()) > 0)
            {
                isSelect = true;
            }
        }

        if (isSelect)
        {
            image.overrideSprite = levelBG; //代码自动替换，把锁头图片替换成可点击图片
            transform.Find("Num").gameObject.SetActive(true);
            int count = PlayerPrefs.GetInt("Level" + gameObject.name); //获取现在关卡对应的名字，然后获得对应的星星数量
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    stars[i].SetActive(true);
                }
            }
        }
    }

    public void Selected() // 这个针对的是点完大关卡以后，12个单个的小关卡
    {
        if (isSelect)
        {
            PlayerPrefs.SetString("NowLevel", "Level" + gameObject.name); //将每一个关卡的名字，添加到键值对集合中
            SceneManager.LoadScene(2);
        }
    }
}