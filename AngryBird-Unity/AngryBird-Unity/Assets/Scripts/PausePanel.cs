using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private Animator anim;
    public GameObject pauseButton;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Retry()
    {
        
    }

    /// <summary>
    /// 点击了 Pause按钮
    /// </summary>
    public void PauseGame()
    {
       // 1,播放动画
       // 2，暂停整个游戏画面
        anim.SetBool("IsPause",true);
        pauseButton.SetActive(false); // 暂停游戏画面的时候，隐藏掉游戏画面中暂停的那个按钮
    }

    public void HomeMune()
    {
        
    }

    /// <summary>
    /// 点击了继续的按钮，然后继续游戏
    /// </summary>
    public void ResumeGame()
    {
        // 1,播放 Resume 动画
        Time.timeScale = 1;
        anim.SetBool("IsPause",false);
    }

    /// <summary>
    /// 动画播放完了以后，调用
    /// </summary>
    public void PauseAnimationEnd()
    {
        Time.timeScale = 0;//这就是游戏暂停的写法
    }

    /// <summary>
    ///  Resume动画播放完了以后调用
    /// </summary>
    public void ResumeAnimationEnd()
    {
        pauseButton.SetActive(true);
    }
}
