using UnityEngine;

public class MiddayTrigger : MonoBehaviour
{
    [SerializeField] string inkScriptIndicator;
    public GameObject DialogueManager;

    private void Start()
    {
        inkScriptIndicator = "Prof";
        DialogueManager = GameObject.Find("DialogueManager");
        DialogueManager.GetComponent<DialogueManagerOriginal>().TriggerDialogue(inkScriptIndicator);
    }
    
}
