﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird
{
    protected override void ShowSkillYellowBirdAccelerate()
    {
        base.ShowSkillYellowBirdAccelerate();
        rg.velocity *= 2;
    }
}
