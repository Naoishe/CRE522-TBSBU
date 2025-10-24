using UnityEngine;

public class InteractableCharacter : InteractableObject
{
    delegate void InteractableObjDelegate();
    InteractableObjDelegate IntObDelegate;
    [SerializeField] GameObject[] dialogues;
    [SerializeField] GameObject[] playerDialogues;
    private bool interactionRunning;
    private int n;
    private int pN;

    public void Start()
    {
        IntObDelegate += CalcDistance;
        dialogues[0].SetActive(false);
        interactionRunning = false;
        n=dialogues.Length;
        pN = playerDialogues.Length;  

    }

    public void Update()
    {
        IntObDelegate();
        if (activateInteraction)
        {
            if (!interactionRunning)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactionRunning = true;
                    IntroDialogue();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactionRunning = false;
                    dialogues[n-1].SetActive(false);
                }
            }

        }
        else
        {
            interactionRunning = false;
            dialogues[0].SetActive(false);
        }
    }

    private void IntroDialogue()
    {
        dialogues[0].SetActive(true);
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerDialogues[0].SetActive(interactionRunning);
            playerDialogues[2].SetActive(interactionRunning);   //provides dialogue option buttons if player is still within requirements for interaction
        }
    }
}
