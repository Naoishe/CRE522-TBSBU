using Unity.VisualScripting;
using UnityEngine;

public class TempMenu : MonoBehaviour
{
    public GameObject thisScreen;
    public GameObject timeWid;

    private void Start()
    {
        thisScreen.SetActive(true);
    }
    public void ButtonPress()
    {
        thisScreen.SetActive(false);
        timeWid.transform.position = new Vector3(373, 975, 0);
    }
}
