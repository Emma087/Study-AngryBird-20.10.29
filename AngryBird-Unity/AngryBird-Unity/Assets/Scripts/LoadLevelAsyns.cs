using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAsyns : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(1280,720,false);
        //上面代码，限制了屏幕的大小，参数，宽，高，是否全屏
        Invoke("Load", 2); 
        //因为异步加载那个太快了（是因为我们场景东西少），所以使用延迟的方法展示
    }

    void Load()
    {
        SceneManager.LoadSceneAsync(1); //这是一个异步加载的自带函数方法
    }
}