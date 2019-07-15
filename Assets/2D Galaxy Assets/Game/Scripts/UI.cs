using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    public Sprite[] lives;
    public Image liveImageDisplay;
    public Text scoreText;
    public int score;

    public void UpdateLives(int currentLives)
    {
        Debug.Log("Player: " + currentLives);
        liveImageDisplay.sprite = lives[currentLives];  
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
        
    }
}
