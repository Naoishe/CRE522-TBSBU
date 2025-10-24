using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Collectable : Player
{
    public int currentCollectables;
    public Text scoreText;

    void Start()
    {
        currentCollectables= 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentCollectables=currentCollectables+1;
            scoreText.text="Orbs: " + scoreText.ToString();
        }
    }
}
