using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LivesManager : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player's movement
    public int startingLives = 3; // Starting number of lives
    public TextMeshProUGUI livesText; // Text canvas to display remaining lives
    public GameObject winningCanvas;
    public GameObject gameCanvas;
    private int currentLives; // Current number of lives

    private void Start()
    {
        currentLives = startingLives; // Set the current lives to the starting lives
        UpdateLivesText(); // Update the text canvas with the initial number of lives
    }

    private void Update()
    {
        // Player movement logic goes here...

        // Example: Move the player horizontally
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);
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
