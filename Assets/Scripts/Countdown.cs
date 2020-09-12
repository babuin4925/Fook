using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    private Action timerAction;
    public float sec;
    void Update()
    {
        if (sec > 0f) 
        { 
            sec -= Time.deltaTime; 
            if (TimerIsComplete())
            {
                timerAction();
            }
        }
    }
    
    public void SetTimer(float seconds, Action myAction)
    {
        timerAction = myAction;
        sec = seconds; 
    }
    
    private bool TimerIsComplete()
    {
        return sec<=0;
    }
}