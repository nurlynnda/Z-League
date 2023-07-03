using Photon.Pun;
using UnityEngine;

public class BallSpawn : MonoBehaviourPunCallbacks
{
    public GameObject ballPrefab;
    public Transform spawnPoint;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(ballPrefab.name, spawnPoint.position, Quaternion.identity);
        }
    }
}