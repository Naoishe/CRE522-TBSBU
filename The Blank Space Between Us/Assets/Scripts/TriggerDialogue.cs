using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;

public class TriggerDialogue : InteractableObject
{
    public static Action InteractionsComplete;
    public TextAsset inkFile;
    public GameObject textBox;
    public GameObject customButton;
    public GameObject optionPanel;
    public bool isTalking = false;
    public Text message;
    private int counter = 0;
    private bool allowDialogue;

    static Story story;
    List<string> tags;
    static Choice choiceSelected;
    private bool sentenceEnded;

    public void Start()
    {
        story = new Story(inkFile.text);
        tags = new List<string>();
        choiceSelected = null;
        allowDialogue = false;
    }
    public override void Interaction()
    {
           textBox.SetActive(true);
           counter = 0;
    }

    public void Dialogue()
    {
        allowDialogue = true;
    }

    void Update()
    {
        if (allowDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space) || counter == 0)
            {
                counter = counter + 1;
                //Checking if there is more of the story left
                if (story.canContinue) //canContinue checks to see if there's more to the story
                {
                    //nametag.text = "Professor:";
                    AdvanceDialogue();

                    ///checking if there is a choice
                    if (story.currentChoices.Count != 0)
                    {
                        StartCoroutine(ShowChoices());
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
        Debug.Log("End---");
        textBox.SetActive(false);
        allowDialogue = false;
        ContinuousData.instance.UpdateInteractionCount();
        if (ContinuousData.instance.interactionsHad >= 5)
        {
            InteractionsComplete?.Invoke();
        }

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
        foreach (char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return null;
        }


        yield return null;
    }

    IEnumerator ShowChoices()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Choices Presented."); List<Choice> _choices = story.currentChoices;

        for (int i = 0; i < _choices.Count; i++)
        {
            GameObject temp = Instantiate(customButton, optionPanel.transform);
            temp.transform.GetChild(0).GetComponent<Text>().text = _choices[i].text;
            temp.AddComponent<DiaSelectable>();
            temp.GetComponent<DiaSelectable>().element = _choices[i];
            temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<DiaSelectable>().Decide(); });
        }

        optionPanel.SetActive(true);
        yield return new WaitUntil(() => { return choiceSelected != null; });

        AdvanceFromDecision();
    }

    public static void SetDecision(object element)
    {
        choiceSelected = (Choice)element;
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    void AdvanceFromDecision()
    {

        optionPanel.SetActive(false);
        for (int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }
        choiceSelected = null;
        AdvanceDialogue();
    }
}
