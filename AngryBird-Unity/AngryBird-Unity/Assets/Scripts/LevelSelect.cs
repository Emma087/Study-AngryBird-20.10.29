using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public bool isSelect = false; //是否已经解锁，当前是否可以点击
    public Sprite levelBG; //可以点击以后的，切换的那个不带锁的图片
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();//这个能获得引擎中的那张图片资源
    }

    private void Start()
    {
        if(transform.parent.GetChild(0).name == gameObject.name)
        {
            isSelect = true;
        }

        if (isSelect)
        {
            image.overrideSprite = levelBG; //代码自动替换，把锁头图片替换成可点击图片
            transform.Find("Num").gameObject.SetActive(true);
        }
    }

    public void Selected() // 这个针对的是点完大关卡以后，12个小关卡挂载的方法脚本
    {
        if (isSelect)
        {
            
        }
    }
}
