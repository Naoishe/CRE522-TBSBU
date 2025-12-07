using UnityEngine;

public class TempMenu : MonoBehaviour
{
    public GameObject thisScreen;
    public GameObject startFixMovedObject;

    private void Start()
    {
        thisScreen.SetActive(true);
    }
    public void ButtonPress()
    {
        thisScreen.SetActive(false);
        startFixMovedObject.transform.position = new Vector3(411,989, 0);
    }
}
