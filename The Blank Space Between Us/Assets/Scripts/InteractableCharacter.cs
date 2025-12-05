//using UnityEngine;
//using System.Collections;

//public class InteractableCharacter : InteractableObject
//{
    
//    [SerializeField] GameObject[] dialogues;
//    [SerializeField] GameObject[] playerDialogues;
//    private bool interactionRunning;
//    [SerializeField] GameObject hint1;
//    private int n;
//    private int pN;

//    public void Start()
//    {
        
//        dialogues[0].SetActive(false);
//        interactionRunning = false;
//        n=dialogues.Length;
//        pN = playerDialogues.Length;  
//        hint1.SetActive(false);

//    }

//    public void Update()
//    {
//        CalcDistance();
//        if (activateInteraction)
//        {
//            Debug.Log("Here");
//            hint1.SetActive(true);

//            if (!interactionRunning)
//            {
//                if (Input.GetKeyDown(KeyCode.E))
//                {
//                    interactionRunning = true;
//                    IntroDialogue();
//                }
//            }
//            else
//            {
//                if (Input.GetKeyDown(KeyCode.E))
//                {
//                    interactionRunning = false;
//                    dialogues[n-1].SetActive(false);
//                }
//            }

//        }
//        else
//        {
//            interactionRunning = false;
//            dialogues[0].SetActive(false);
//            hint1.SetActive(false);
//        }
//    }

//    private void IntroDialogue()
//    {
//        dialogues[0].SetActive(true);
//        if (Input.GetKeyDown(KeyCode.E))
//        {
//            playerDialogues[0].SetActive(interactionRunning);
//            playerDialogues[1].SetActive(interactionRunning);   //provides dialogue option buttons if player is still within requirements for interaction
//        }
//    }

//    public void GoodAnswer()
//    {
//        playerDialogues[0].SetActive(false);
//        dialogues[1].SetActive(true);
//        StartCoroutine(EndInteractionA());
//    }

//    public void BadAnswer()
//    {
//        playerDialogues[1].SetActive(false);
//        dialogues[2].SetActive(true);
//        StartCoroutine(EndInteractionB());
//    }

//    private IEnumerator EndInteractionA()
//    {
//        yield return new WaitForSeconds(3);
//        dialogues[1].SetActive(false);

       
//    }
//    private IEnumerator EndInteractionB()
//    {
//        yield return new WaitForSeconds(3);
//        dialogues[2].SetActive(false);


//    }
//}
