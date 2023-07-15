using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LivesManager : MonoBehaviour
{
    public int startingLives = 3; // Starting number of lives
    public TMP_Text livesText; // Text canvas to display remaining lives
    public GameObject winningCanvas;
    public GameObject gameCanvas;
    private int currentLives; // Current number of lives

    private void Start()
    {
        currentLives = startingLives; // Set the current lives to the starting lives
        UpdateLivesText(); // Update the text canvas with the initial number of lives
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collided with a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Decrease the number of lives
            currentLives--;

            // Update the text canvas with the new number of lives
            UpdateLivesText();

            // Check if the player is out of lives
            if (currentLives <= 0)
            {
                // Player is out of lives - perform game over logic
                GameOver();
            }
        }
    }

    private void UpdateLivesText()
    {
        // Update the text canvas with the current number of lives
        livesText.text = "Lives: " + currentLives.ToString();
    }

    private void GameOver()
    {
        winningCanvas.SetActive(true);
        gameCanvas.SetActive(false);
    }
}
