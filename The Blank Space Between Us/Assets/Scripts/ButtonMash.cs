using UnityEngine;
using System.Collections;

public class ButtonMash : InteractableObject
{

    [SerializeField] GameObject introDialogue;
    [SerializeField] GameObject spamDialogue;
    [SerializeField] GameObject GameOverDialogue;
    [SerializeField] GameObject Hint;

    private float timeA;
    private float timeB;
    private float timeC;
    private float differenceA;
    private float differenceB;
    private bool holdNewComparison=false;
    private bool miniGameStarted = false;
    private int collectedVariables = 0;
    public double percIncrement;
    private bool timerEnabled = false;
    public bool roundEnded = false;


    public double playerScore=0;
    public double goal = 100.00;

    private void Start()
    {
       
    }
    private void Update()
    {
        //CalcDistance();
        if (objectActive)
        {

            if (Input.GetKeyDown(KeyCode.E)) 
            { 
                miniGameStarted= true;
            }
            if (miniGameStarted)
            {
                if (timerEnabled)
                {
                    ButtonMashGameLoop();
                }


            }
            else
            {
                introDialogue.SetActive(true);
                StartCoroutine(IntroCD());
            }

        }
        
        
        if (roundEnded)
        {
            GameOver();
        }
        
        
    }

    private void GameOver()
    {
        GameOverDialogue.SetActive(true);
        objectActive = false;
        miniGameStarted = false;
        DebugResults();
    }
    private void DebugResults()
    {
        Debug.Log("Player Scored: " + playerScore + " points!");
        StartCoroutine(DisableEndDialogueBox());
    }

    private IEnumerator IntroCD()
    {
        yield return new WaitForSeconds(3);
        introDialogue.SetActive(false);
        spamDialogue.SetActive(true);
        yield return new WaitForSeconds(2);
        spamDialogue.SetActive(false);

    }

    private IEnumerator DisableEndDialogueBox()
    {
        yield return new WaitForSeconds(4);
        GameOverDialogue.SetActive(false);
        playerScore = 0;
    }

    private void ButtonMashGameLoop()
    {
        //Get player button inputs
        if (!holdNewComparison)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                switch (collectedVariables)
                {
                    case 0:
                        StartCoroutine(FirstCheck());
                        break;
                    case 1:
                        StartCoroutine(SecondCheck());
                        break;
                    case 2:
                        StartCoroutine(ThirdCheck());
                        break;
                    case 3:
                        break;
                }
            }
        }
    }

    private IEnumerator TimerCountdown()
    {
        timerEnabled= true;
        yield return new WaitForSeconds(5);
        timerEnabled= false;
        roundEnded = true;
    }

    private IEnumerator FirstCheck()
    {
        holdNewComparison = false;
        collectedVariables = 1;
        timeA = Time.fixedDeltaTime;
        yield return null;
    }

    private IEnumerator SecondCheck()
    {
        timeB = Time.fixedDeltaTime;
        yield return null;
    }
    private IEnumerator ThirdCheck()
    {
        timeC = Time.fixedDeltaTime;
        collectedVariables = 3;
        holdNewComparison = true;
        CalcPercentage();
        yield return null;
    }

    private void CalcPercentage()
    {
        differenceA = timeB - timeA;
        differenceB = timeC - timeB;
        
        if (differenceA > differenceB) //faster
        {
            percIncrement=(differenceB/differenceA)*100;
        }
        if(differenceB> differenceA) //slower
        {
            percIncrement = -((differenceB / differenceA) * 100);
        }
        else
        {
            percIncrement = 0; //no alteration in rate
        }
        UpdateScore();
        holdNewComparison= false;
    }

    private void UpdateScore()
    {
        playerScore=playerScore+(playerScore*percIncrement);
    }

}
