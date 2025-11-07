using UnityEngine;

public class IO_Chair : InteractableObject
{

    public void Start()
    {
        standardNotifications = true;

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) ///temp
        {
            if(objectActive)
            {
                EndInteraction();
            }
        }
       
    }

    public override void Interaction()
    {
        ///
    }
    
     ////////////Need to have a way so when the player presses E but an interaction with an object is already active, it goes to a 'continue' option instead of the activation method
     ////// Check IntObDelegate flows correctly
     ///Program 'standardNotifications'
}
