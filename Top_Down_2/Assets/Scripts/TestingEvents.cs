using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingEvents : MonoBehaviour
{
    public event EventHandler<OnSpacePressedEventArgs> OnSpacePressed;

    void Start()
    {
        OnSpacePressed += Testing_OnSpacePressed;
    }

    public class OnSpacePressedEventArgs : EventArgs
    {
        public int spaceCount;
    }
    private int spaceCount;
    private void Testing_OnSpacePressed(object sender, EventArgs e)
    {
        Debug.Log("SPACE");
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            spaceCount++;
            OnSpacePressed?.Invoke(this, new OnSpacePressedEventArgs { spaceCount = spaceCount });
        }
    }
} 
