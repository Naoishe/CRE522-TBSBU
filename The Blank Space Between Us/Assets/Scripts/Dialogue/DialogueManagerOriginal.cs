using Ink.Runtime;
using Ink.UnityIntegration;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManagerOriginal : MonoBehaviour
{
    public static Action DialogueScriptFinished;

    private TextAsset usedInkFile;
    public TextAsset MiddayinkFile;
    public TextAsset NikoinkFile;
    public TextAsset FaustinkFile;
    public TextAsset GenesisinkFile;
    public TextAsset LumiinkFile;
    public TextAsset SaleminkFile;

    public GameObject textBox;
    public GameObject customButton;
    public GameObject optionPanel;
    public bool isTalking = false;
    public Text message;

    static Story story; //main class that contains ink file and current state of the story
    //List<string> tags;
    static Choice choiceSelected; //potential choices
    private bool choicespresented;
    //private bool dialogueEnabled;
    private bool classScene;
  
    void Start()
    {
        //story = new Story(inkFile.text);  
        //tags =  new List<string>();
        choiceSelected = null;
        choicespresented = false;
        classScene = false;
    }

    
    void Update()
    {
        if (!choicespresented)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Checking if there is more of the story left
                if (story.canContinue) //canContinue checks to see if there's more to the story
                {
                    //nametag.text = "Professor:";
                    AdvanceDialogue();

                    ///checking if there is a choice
                    if (story.currentChoices.Count != 0)
                    {
                        StartCoroutine(ShowChoices());
                        choicespresented=true;
                    }
                }
                else
                {
                    FinishDialogue();
                }
            }
        }
        
    }

    private void FinishDialogue()
    {
        //Debug.Log("End---");
        if (classScene)
        {
            DialogueScriptFinished?.Invoke();
        }
        //dialogueEnabled = false;
        textBox.SetActive(false);
        
       
    }

    void AdvanceDialogue()
    {
        string currentSentence = story.Continue(); //fetches the next sentence in the file
        //ParseTags(); COME BACK TO THIS WHEN UPDATED SCRIPTS WITH TAG CALLS
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
        
    }

    IEnumerator TypeSentence(string sentence)
    {
        message.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            message.text+= letter;
            yield return null;
        }
        

        yield return null;
    }

    IEnumerator ShowChoices()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Choices Presented.");
        List<Choice> _choices = story.currentChoices;
        optionPanel.SetActive(true);

        for (int i = 0; i < _choices.Count; i++)
        {
            GameObject temp = Instantiate(customButton, optionPanel.transform);
            temp.transform.GetChild(0).GetComponent<Text>().text = _choices[i].text;
            temp.AddComponent<DiaSelectable>();
            temp.GetComponent<DiaSelectable>().element = _choices[i];
            temp.GetComponent<Button>().onClick.AddListener(() => {temp.GetComponent<DiaSelectable>().Decide(); });
        }
        yield return new WaitUntil (() => { return choiceSelected!= null; });

        AdvanceFromDecision();
    }

    public static void SetDecision(object element)
    {
        choiceSelected=(Choice)element;
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    void AdvanceFromDecision()
    {
        
        optionPanel.SetActive(false);
        for(int i = 0; i < optionPanel.transform.childCount; i++) 
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }
        choiceSelected = null;
        choicespresented = false;
        AdvanceDialogue();
    }

    public void TriggerDialogue(string indicator)
    {
        textBox.SetActive(true);
        if (indicator == "Niko")
        {
            usedInkFile = NikoinkFile;
        }
        if (indicator == "Faust")
        {
            usedInkFile = FaustinkFile;
        }
        if (indicator == "Genesis")
        {
            usedInkFile = GenesisinkFile;
        }
        if (indicator == "Lumi")
        {
            usedInkFile = LumiinkFile;
        }
        if (indicator == "Salem")
        {
            usedInkFile = SaleminkFile;
        }
        if (indicator == "Prof")
        {
            usedInkFile = MiddayinkFile;
            classScene = true;
        }
        story = new Story(usedInkFile.text);
        //dialogueEnabled =true;
    }

    /*void ParseTags()
    {
        tags = story.currentTags;
        foreach (string t in tags)
        {
            string prefix = t.Split(' ')[0];
            string param = t.Split(' ')[1];

            switch (prefix.ToLower())
            {
                case "anim":
                    SetAnimation(param);
                    break;

                case "color":
                    SetTextColor(param);
                    break;
            }
        }

    }*/
}
