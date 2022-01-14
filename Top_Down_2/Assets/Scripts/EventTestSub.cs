using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTestSub : MonoBehaviour
{
    
    private void Start()
    {
        TestingEvents testingEvents = GetComponent<TestingEvents>();
        testingEvents.OnSpacePressed += TestingEvents_OnSpacePressed;
    }

    private void TestingEvents_OnSpacePressed(object sender, TestingEvents.OnSpacePressedEventArgs e)
    {
        Debug.Log("Space from Sub" + e.spaceCount);
    }
    void Update()
    {
        
    }
}
