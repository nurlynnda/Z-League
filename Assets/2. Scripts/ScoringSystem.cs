using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{

    public GameObject scoreText;
    public int theScore;
    public AudioSource collectSound;

    void OnTriggerEnter(Collider other)
    {
        collectSound.Play();
        theScore += 50;
        scoreText.GetComponent<Text>().text = "SCORE: " + theScore;
        Destroy(gameObject);
    }
}

