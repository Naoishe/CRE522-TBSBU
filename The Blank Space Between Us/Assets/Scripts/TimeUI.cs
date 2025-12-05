using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class TimeUI : MonoBehaviour
{
    
    public TextMeshProUGUI timeText;
    //public TimeManager timeManager;
    private int timeIndex;
    private int day;

    public void Update()
    {
        timeIndex = TimeManager.TimeFrameIndex;  
        day= TimeManager.Day;
        UpdateTime();
    }
    
    private void UpdateTime()
    {
        timeText.text = $"Day {day}: {TimeManager.TimeFrame[timeIndex]}";
        //Debug.Log("TimeUpdated");
        //Debug.Log("timeIndex = " + timeIndex);
        //Debug.Log("Day = " + day);
    }

}
