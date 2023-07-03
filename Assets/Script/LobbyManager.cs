using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public TMP_Text[] playerName;

    public TMP_InputField roomName;
    //public Text roomName;

    public GameObject roomSettingPanel;
    public GameObject waitingPanel;
    public GameObject teamSelectionPanel;

    public Button startButton;

    RoomOptions options;
    public int maxNumberOfPlayers = 2;
    public int minNumberOfPlayers = 1;

    public string sceneMP;

    private int playerTeamIndex = -1;

    void Start()
    {
        options = new RoomOptions
        {
            MaxPlayers = (byte)maxNumberOfPlayers,
            IsOpen = true,
            IsVisible = true
        };
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == maxNumberOfPlayers)
        {
            startButton.interactable = true;
        }
        else
        {
            startButton.interactable = false;
        }
    }

    public void JoinRoom()
    {
        if (!string.IsNullOrEmpty(roomName.text))
        {
            roomSettingPanel.SetActive(false);
            teamSelectionPanel.SetActive(true);

            string namePlayer = PlayerPrefs.GetString("username");
            PhotonNetwork.NickName = namePlayer;
            PhotonNetwork.JoinOrCreateRoom(roomName.text, options, TypedLobby.Default);
        }
    }

    public void SelectTeamA()
    {
        playerTeamIndex = 0;
        PlayerPrefs.SetInt("PlayerTeamIndex", 0);
        StartGame();
    }

    public void SelectTeamB()
    {
        playerTeamIndex = 1;
        PlayerPrefs.SetInt("PlayerTeamIndex", 1);
        StartGame();
    }

    private void StartGame()
    {
        teamSelectionPanel.SetActive(false);
        waitingPanel.SetActive(true);

        if (playerTeamIndex == 0)
        {
            playerName[0].text = PhotonNetwork.NickName;
            photonView.RPC("Send_PlayerName", RpcTarget.OthersBuffered, 0, PhotonNetwork.NickName);
        }
        else if (playerTeamIndex == 1)
        {
            playerName[1].text = PhotonNetwork.NickName;
            photonView.RPC("Send_PlayerName", RpcTarget.OthersBuffered, 1, PhotonNetwork.NickName);
        }
    }

    public void LoadScene()
    {
        PlayerPrefs.SetString("PlayerName1", playerName[0].text);
        PlayerPrefs.SetString("PlayerName2", playerName[1].text);
        PhotonNetwork.LoadLevel(sceneMP);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        roomSettingPanel.SetActive(false);
        teamSelectionPanel.SetActive(true);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        throw new System.NotImplementedException();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        throw new System.NotImplementedException();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        throw new System.NotImplementedException();
    }

    [PunRPC]
    void Send_PlayerName(int index, string name)
    {
        playerName[index].text = name;
    }
}
