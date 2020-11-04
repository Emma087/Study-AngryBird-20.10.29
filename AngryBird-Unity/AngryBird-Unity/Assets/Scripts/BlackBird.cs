using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{
    List<Pig> blocks = new List<Pig>();
    // 黑鸟在引擎添加了一个区域超大的 collider，把每一个进入范围的敌人都记录在 List 内，超出的移除

    /// <summary>
    /// 进入触发的区域
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy") 
        {
            blocks.Add(other.gameObject.GetComponent<Pig>());
            // for (int i = 0; i < blocks.Count; i++)
            // {
            //     Debug.Log(blocks[i]);
            // }
        }
    }

    /// <summary>
    /// 离开触发区域
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy") 
        {
            blocks.Remove(other.gameObject.GetComponent<Pig>());
        }
    }

    protected override void ShowSkillYellowBirdAccelerate()
    {
        base.ShowSkillYellowBirdAccelerate();
        if (blocks.Count > 0 && blocks != null)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Dead(); //这个 dead 函数是猪/木板的死亡函数
            }
            // foreach (var block in blocks) 注意针对 List 不能用 foreach 因为会涉及到更改值，List 属于只读，这里不确定
        }
        OnClearEffect();
    }

    void OnClearEffect() //清楚掉所有的效果，拖尾等等，还有自己的位置停下不动
    {
        rg.velocity = Vector3.zero; //速度等于0，限制住行动
        Instantiate(boomBird, transform.position, Quaternion.identity);
        render.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        myTrail.TrailsClear();//清除拖尾
    }

    protected override void NextBirdReadyAndCurrentBirdDeath()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        GameManager._instance.NextBird();
    }
}