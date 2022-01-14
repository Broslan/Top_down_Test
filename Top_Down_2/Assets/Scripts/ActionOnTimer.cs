using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOnTimer : MonoBehaviour
{
    private float timer;
    
    private void Start()
    {
        FunctionTimer1.Create(WriteSomething, 3f);
        FunctionTimer1.Create(WriteSomething2, 4f);
    }

   private void WriteSomething()
    {
        Debug.Log("WriteSomething");
    }

    private void WriteSomething2()
    {
        Debug.Log("WriteSomething2");
    }

    public bool IsTimerComplete()
    {
        return timer <= 0f;
    }
}
