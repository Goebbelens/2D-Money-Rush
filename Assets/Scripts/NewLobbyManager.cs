using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;
using UnityEngine.UI;

public class NewLobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject findRoomPanel;

    [SerializeField]
    private GameObject createRoomPanel;

    [SerializeField]
    private TextMeshProUGUI findRoomName;

    [SerializeField]
    private TextMeshProUGUI createRoomName;

    private void Start()
    {
        findRoomPanel.SetActive(false);
        createRoomPanel.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("We are connected to Photon! on " + PhotonNetwork.CloudRegion + " Server");
        PhotonNetwork.AutomaticallySyncScene = true; 
        PhotonNetwork.JoinLobby();
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions =
        new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 4
        };

        Hashtable RoomCustomProps = new Hashtable
        {
            { "P1SCORE", 0 },
            { "P2SCORE", 0 },
            { "P3SCORE", 0 },
            { "P4SCORE", 0 },
            { "REMAIN", 0 } //////////////////////////////////////////////
        };
        roomOptions.CustomRoomProperties = RoomCustomProps;

        PhotonNetwork.CreateRoom(createRoomName.text, roomOptions);
        Debug.Log("Room created, Waiting for other players");
    }

    public void FindRoom()
    {
        PhotonNetwork.JoinRoom(findRoomName.text);
        Debug.Log("Searching for a Game (Room: " + findRoomName.text + ")");
    }

    public override void OnJoinedLobby()
    {
        findRoomPanel.SetActive(true);
        createRoomPanel.SetActive(true);

        Debug.Log("Lobby joined successfully");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Could not Find Room");
    }
    

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2 && PhotonNetwork.IsMasterClient)
        {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount + "/4 Starting Game");

            PhotonNetwork.LoadLevel(1);
        }
    }
}
