using Unity.VisualScripting;
using UnityEngine;

public class TempMenu : MonoBehaviour
{
    public GameObject thisScreen;

    private void Start()
    {
        thisScreen.SetActive(true);
    }
    public void ButtonPress()
    {
        thisScreen.SetActive(false);
    }
}
