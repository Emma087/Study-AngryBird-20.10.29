using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaa : MonoBehaviour
{
    private void Reset()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            transform.GetChild(i).name = (i + 1).ToString();
        }
    }
}
