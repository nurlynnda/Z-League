using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class Goal : MonoBehaviourPunCallbacks, IPunObservable
{
    public TextMeshProUGUI scoreText;
    public int score = 0;
    public Transform spawnPoint;

    public AudioClip clip;

    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            score++;
            scoreText.text = score.ToString();

            Rigidbody ballRb = other.GetComponent<Rigidbody>();
            ballRb.velocity = Vector3.zero;
            ballRb.angularVelocity = Vector3.zero;
            other.gameObject.transform.position = spawnPoint.position;

            audio.clip = clip;
            audio.Play();
        }

        if (PhotonNetwork.IsConnected)
        {
            // Synchronize the score across all players
            photonView.RPC("UpdateScore", RpcTarget.AllBuffered, score);
        }

    }

    [PunRPC]
    void UpdateScore(int newScore)
    {
        score = newScore;
        scoreText.text = score.ToString();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send the score over the network
            stream.SendNext(score);
        }
        else if (stream.IsReading)
        {
            // Receive the score from the network
            int newScore = (int)stream.ReceiveNext();
            score = newScore;
            scoreText.text = score.ToString();
        }
    }
}