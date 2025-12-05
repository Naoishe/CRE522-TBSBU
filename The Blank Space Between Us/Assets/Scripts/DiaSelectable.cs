using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using Ink.Runtime;
using UnityEngine.UI;

public class DiaSelectable : MonoBehaviour
{
    public object element;
    public void Decide()
    {
        DialogueManagerOriginal.SetDecision(element);
    }
}
