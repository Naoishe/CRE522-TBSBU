using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Day1Control : MonoBehaviour
{

    public Collider2D playerCollider;
    public Collider2D exitCollider;
    public Collider2D returnCollider;
    private bool objectivesFinished;

    private void Start()
    {
        objectivesFinished = ObjectiveSkip.objectivesSkipped;
    }
    void Update()
    {
        if (Physics2D.IsTouching(exitCollider, playerCollider))
        {
            TimeManager.OnTimeFrameChanged?.Invoke();
        }
        if (Physics2D.IsTouching(returnCollider, playerCollider) && objectivesFinished)
        {
            TimeManager.OnTimeFrameChanged?.Invoke();
        }

    }

}
