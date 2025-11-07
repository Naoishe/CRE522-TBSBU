using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

public class TimeManager : MonoBehaviour
{
    public static Action OnTimeFrameChanged;
    public static Action OnDayChanged;

    public static string[] TimeFrame = { "Morning", "Midday", "Early Evening", "Late Evening", "Night" };
    public static int Day; 

    public static int TimeFrameIndex; 
    void Start()
    {
        Day = 0;
        TimeFrameIndex= 0;
    }

    private void OnEnable()
    {
        OnTimeFrameChanged += UpdateTimeFrame;
    }

    private void OnDisable()
    {
        OnTimeFrameChanged -= UpdateTimeFrame;
    }


    void Update()
    {
        ///Temporary testing trigger for development to enable time updates:
        if(Input.GetKeyUp(KeyCode.T))
        {
            OnTimeFrameChanged?.Invoke();
        }
    }


    private void UpdateTimeFrame()
    {
        if (TimeFrameIndex == 4)
        {
            TimeFrameIndex = 0;
            Day++;
            OnDayChanged?.Invoke();
            Debug.Log("Current day:" + Day + " Current TimeframeIndex: " + TimeFrameIndex);
        }
        else
        {
            TimeFrameIndex++;
        }
        
    }
}
