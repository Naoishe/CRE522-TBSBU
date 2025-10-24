using UnityEngine;

public class IO_Chair : InteractableObject
{
    delegate void InteractableObjDelegate();
    InteractableObjDelegate IntObDelegate;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] GameObject dialogue;
    [SerializeField] GameObject hint1;
    private bool interactionRunning;

    public void Start()
    {
        IntObDelegate += CalcDistance;
        dialogueBox.SetActive(false);
        dialogue.SetActive(false);
        interactionRunning = false;

    }

    public void Update()
    {
        IntObDelegate();
        
        if (activateInteraction)
        {
            hint1.SetActive(true);
            if (!interactionRunning)
            {
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactionRunning = true;
                    dialogueBox.SetActive(true);
                    dialogue.SetActive(true);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactionRunning = false;
                    dialogueBox.SetActive(false);
                    dialogue.SetActive(false);
                }
            }

        }
        else
        {
            hint1.SetActive(false);
            interactionRunning = false;
            dialogueBox.SetActive(false);
            dialogue.SetActive(false);
        }
    }
}
