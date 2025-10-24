using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    [SerializeField] GameObject player;
    public int currentCollectables;
    public Text scoreText;

    void Start()
    {
        currentCollectables = 0;
    }


    private void Update()
    {
        scoreText.text = currentCollectables.ToString();
    }
}
