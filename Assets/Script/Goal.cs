using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Goal : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            UpdateScore();

            Rigidbody ballRb = other.GetComponent<Rigidbody>();
            ballRb.velocity = Vector3.zero;
            ballRb.angularVelocity = Vector3.zero;
            other.gameObject.transform.position = spawnPoint.position;
        }
    }

    [PunRPC]
    void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
