using UnityEditor.MPE;
using UnityEngine;

public class WorkAround : MonoBehaviour
{
    [SerializeField] public GameObject thisChar;
    [SerializeField] string inkScriptIndicator;
    public GameObject player;
    public GameObject DialogueManager;
    private float distance;
    private bool prevent;

    private void Start()
    {
        DialogueManager = GameObject.Find("DialogueManager");
        player = GameObject.Find("PlayerObj");
        prevent = false;
    }
    void Update()
    {
        TempMethod();
    }

    public void TempMethod()
    {
        distance = Vector2.Distance(player.transform.position, thisChar.transform.position);
        if (distance < 5 && Input.GetKeyDown(KeyCode.E) && prevent==false)
        {
            WorkAroundTrigger();
            prevent = true;
        }
    }
    public void WorkAroundTrigger()
    {
        DialogueManager.GetComponent<DialogueManagerOriginal>().TriggerDialogue(inkScriptIndicator);
    }
}
