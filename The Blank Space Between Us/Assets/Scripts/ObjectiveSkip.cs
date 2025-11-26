using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectiveSkip : InteractableObject
{
    public static bool objectivesSkipped;
    private bool objectUsed;

    private void Start()
    {
        objectivesSkipped = false; 
    }
    void Update()
    {
        if(objectActive)
        {
            if(objectUsed==false)
            {
                Console.WriteLine("Objectives Skipped, Time Updated");
                objectivesSkipped = true;
                objectUsed = true;
            }
            
        }
    }
}
