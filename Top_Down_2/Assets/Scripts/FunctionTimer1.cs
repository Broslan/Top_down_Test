using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTimer1
{

    private static List<FunctionTimer1> activeTimerList;
    private static GameObject initGameObject;

    private static void InitIfNeeded()
    {
        if(initGameObject == null)
        {
            initGameObject = new GameObject("FunctionTimer1");
            activeTimerList = new List<FunctionTimer1>();
        }
    }
    public static FunctionTimer1 Create(Action action, float timer, string timerName = null)
    {
        InitIfNeeded();

        GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));

        FunctionTimer1 functionTimer1 = new FunctionTimer1(action, timer, timerName, gameObject);

        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer1.Update;

        activeTimerList.Add(functionTimer1);

        return functionTimer1;
    }

    private static void StopTimer(string timerName)
    {
        for (int i = 0; i < activeTimerList.Count; i++)
        {
            if(activeTimerList[i].timerName == timerName)
            {
                //stop this timer
                activeTimerList[i].DestroySelf();
                i--;
            }
        }
    }
    private static void RemoveTimer(FunctionTimer1 functionTimer1)
    {
        InitIfNeeded();
        activeTimerList.Remove(functionTimer1);
    }
    private class MonoBehaviourHook : MonoBehaviour{
        public Action onUpdate;
        private void Update()
        {
            if (onUpdate != null) onUpdate();
        }
    }

    private Action action;
    private float timer;
    private string timerName;
    private bool isDestroid;
    private GameObject gameObject;

    private FunctionTimer1(Action action, float timer,string timerName, GameObject gameObject)
    {
        this.action = action;
        this.timer = timer;
        this.gameObject = gameObject;
        this.timerName = timerName;
        isDestroid = false;
    }

    public void Update()
    {
        if (!isDestroid)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                action();
                DestroySelf();
            }
        }
    }

    private void DestroySelf()
    {
        isDestroid = true;
        UnityEngine.Object.Destroy(gameObject);
        RemoveTimer(this);
    }
}
   
