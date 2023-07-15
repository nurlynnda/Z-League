using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    public TMP_Text[] playerName;
    public GameObject[] player;
    public Vector3[] playerPos;

    public GameObject[] lines;

    // Start is called before the first frame update
    void Start()
    {
        int master = PlayerPrefs.GetInt("MasterIndex");
        int client = PlayerPrefs.GetInt("ClientIndex");

        if (PhotonNetwork.IsMasterClient)
        {
            playerName[master].text = PhotonNetwork.NickName;
            lines[master].SetActive(true);
            photonView.RPC("Set_OtherPlayerName", RpcTarget.OthersBuffered, master, PhotonNetwork.NickName);
            Quaternion rotation = Quaternion.Euler(0f, master == 1 ? 180f : 0f, 0f);
            PhotonNetwork.Instantiate(player[master].name, playerPos[master], rotation);

        }
        else
        {
            playerName[client].text = PhotonNetwork.NickName;
            lines[client].SetActive(true);
            photonView.RPC("Set_OtherPlayerName", RpcTarget.OthersBuffered, client, PhotonNetwork.NickName);
            Quaternion rotation = Quaternion.Euler(0f, client == 1 ? 180f : 0f, 0f);
            PhotonNetwork.Instantiate(player[client].name, playerPos[client], rotation);

        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    [PunRPC]
    void Set_OtherPlayerName(int index, string name)
    {
        playerName[index].text = name;
    }
}