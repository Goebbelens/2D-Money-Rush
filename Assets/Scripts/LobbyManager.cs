using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject findMatchBtn;

    [SerializeField]
    private GameObject searchingPanel;

    private void Start()
    {
        searchingPanel.SetActive(false);
        findMatchBtn.SetActive(false);

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are connected to Photon! on " + PhotonNetwork.CloudRegion + " Server");
        PhotonNetwork.AutomaticallySyncScene = true;
        findMatchBtn.SetActive(true);
    }

    public void FindMatch()
    {
        searchingPanel.SetActive(true);
        findMatchBtn.SetActive(false);

        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Searching for a Game");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Could not Find Room - Creating a Room");
        MakeRoom();
    }

    private void MakeRoom()
    {
        int randomRoomName = Random.Range(0, 5000);
        RoomOptions roomOptions =
        new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2
        };

        Hashtable RoomCustomProps = new Hashtable();
        RoomCustomProps.Add("P1SCORE", 0);
        RoomCustomProps.Add("P2SCORE", 0);
        RoomCustomProps.Add("P3SCORE", 0);
        RoomCustomProps.Add("P4SCORE", 0);
        roomOptions.CustomRoomProperties = RoomCustomProps;

        PhotonNetwork.CreateRoom("RoomName_" + randomRoomName, roomOptions);
        Debug.Log("Room created, Waiting for another player");
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount + "/2 Starting Game");

            PhotonNetwork.LoadLevel(1);
        }
    }


    public void StopSearch()
    {
        searchingPanel.SetActive(false);
        findMatchBtn?.SetActive(true);
        PhotonNetwork.LeaveRoom();
        Debug.Log("Stopped, Back to Menu");
    }
}
