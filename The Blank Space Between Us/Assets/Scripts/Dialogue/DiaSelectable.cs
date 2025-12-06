using UnityEngine;

public class DiaSelectable : MonoBehaviour
{
    public object element;
    public void Decide()
    {
        DialogueManagerOriginal.SetDecision(element);
    }
}
