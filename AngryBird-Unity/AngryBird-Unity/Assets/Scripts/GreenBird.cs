using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird
{
    //注意这个绿色鸟的飞的方式，是飞出去，点击屏幕以后成一个锐角的样式往回飞
    protected override void ShowSkillYellowBirdAccelerate()
    {
        base.ShowSkillYellowBirdAccelerate();
        Vector3 speed = rg.velocity; //speed 存储当前鸟的速度，
        speed.x *= -1; //给鸟加一个反方向的力，Y不变，X乘-1
        rg.velocity = speed; //再把原来的速度赋值回去
    }
}
