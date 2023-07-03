using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    public TMP_Text[] playerName;
    public GameObject player;
    public Vector3[] playerPos;

    // Start is called before the first frame update
    void Start()
    {
        int playerTeamIndex = PlayerPrefs.GetInt("PlayerTeamIndex");

        if (PhotonNetwork.IsMasterClient)
        {
            playerName[playerTeamIndex].text = PhotonNetwork.NickName;
            photonView.RPC("Set_OtherPlayerName", RpcTarget.OthersBuffered, playerTeamIndex, PhotonNetwork.NickName);
            PhotonNetwork.Instantiate(player.name, playerPos[playerTeamIndex], Quaternion.identity);
        }
        else
        {
            playerName[playerTeamIndex].text = PhotonNetwork.NickName;
            photonView.RPC("Set_OtherPlayerName", RpcTarget.OthersBuffered, playerTeamIndex, PhotonNetwork.NickName);
            Quaternion rotationB = Quaternion.Euler(0f, 180f, 0f);
            PhotonNetwork.Instantiate(player.name, playerPos[playerTeamIndex], rotationB);
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