using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public Text timerText;
    public GameObject canvas;
    public GameObject ball;

    void Update()
    {
        DisplayTime(timeRemaining);
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            canvas.SetActive(true);
            ball.SetActive(false);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = "Time Remaining: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}