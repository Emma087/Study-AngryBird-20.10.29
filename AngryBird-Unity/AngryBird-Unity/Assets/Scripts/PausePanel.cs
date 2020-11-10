using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    public void HomeMune()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);

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
        if (GameManager._instance.birds.Count > 0) //如果场景中还有小鸟
        {
            if (GameManager._instance.birds[0].isReLeaved == false) //如果没有松开鼠标
            {
                GameManager._instance.birds[0].canMove = false; //则让小鸟不能被点击
            }
        }
    }

    /// <summary>
    /// 点击了继续的按钮，然后继续游戏
    /// </summary>
    public void ResumeGame()
    {
        // 1,播放 Resume 动画
        Time.timeScale = 1;
        anim.SetBool("IsPause",false);
        if (GameManager._instance.birds.Count > 0) //如果场景中还有小鸟
        {
            if (GameManager._instance.birds[0].isReLeaved == false) //如果没有松开鼠标
            {
                GameManager._instance.birds[0].canMove = true; //则让小鸟恢复，可以被点击状态
            }
        }
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
