using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegatTester : MonoBehaviour
{
    public delegate void TestDelegate();
    public Action myAction;
    [SerializeField] private ActionOnTimer actionOnTimer;

    private TestDelegate testDelegateFunction;
    void Start()
    {
       
    }

    private void TimeIsComplete()
    {
        Debug.Log("Timer was completed");
    } 

    void Update()
    {
       
    }

}
