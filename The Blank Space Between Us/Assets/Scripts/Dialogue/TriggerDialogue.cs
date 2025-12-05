using UnityEngine;

public class TriggerDialogue : InteractableObject
{
    [SerializeField] string inkScriptIndicator;
    public GameObject DialogueManager;

    private void Start()
    {
        DialogueManager = GameObject.Find("DialogueManager");
    }

    public override void Interaction()
    {
        DialogueManager.GetComponent<DialogueManagerOriginal>().TriggerDialogue(inkScriptIndicator);
    }
}
