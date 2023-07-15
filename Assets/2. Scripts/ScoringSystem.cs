using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoringSystem : MonoBehaviour
{

    public TMP_Text scoreText;
    public int theScore;
    public AudioSource collectSound;
    public GameObject surpriseElement;
    public CarController carController;

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        collectSound.Play();
        theScore += 50;
        scoreText.text = "SCORE: " + theScore;
        surpriseElement.SetActive(true);
        carController.SetSPActivated(true);
    }
}

