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

    private void Awake()
    {
        TimeManager.OnTimeFrameChanged += UpdateTime;
    }

    public void Update()
    {
        timeIndex = TimeManager.TimeFrameIndex;  
        day= TimeManager.Day;
    }
    private void OnEnable()
    {
        TimeManager.OnTimeFrameChanged += UpdateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnTimeFrameChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        timeText.text = $"Day {day}: {TimeManager.TimeFrame[timeIndex]}";
        //Debug.Log("timeIndex = " + timeIndex);
        //Debug.Log("Day = " + day);
    }

}
