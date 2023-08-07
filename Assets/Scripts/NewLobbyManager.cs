using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;

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



    /*[SerializeField]
    private string findRoomName;

    [SerializeField]
    private string createRoomName;*/

    private void Start()
    {
        findRoomPanel.SetActive(true);
        createRoomPanel.SetActive(true);
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

        Hashtable RoomCustomProps = new Hashtable();
        RoomCustomProps.Add("P1SCORE", 0);
        RoomCustomProps.Add("P2SCORE", 0);
        RoomCustomProps.Add("P3SCORE", 0);
        RoomCustomProps.Add("P4SCORE", 0);
        roomOptions.CustomRoomProperties = RoomCustomProps;

        PhotonNetwork.CreateRoom(createRoomName.text, roomOptions);
        Debug.Log("Room created, Waiting for other players");
    }

    public void FindRoom()
    {
        PhotonNetwork.JoinRoom(findRoomName.text);
        //PhotonNetwork.JoinRandomRoom();
        Debug.Log("Searching for a Game (Room: " + findRoomName.text + ")");
    }

    /////////////////////////////////////////////////////////////////////////

    public override void OnJoinRandomFailed(short returnCode, string message)
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
        Debug.Log("Test");
    }

    /*public void ReadFindRoomName(string s)
    {
        //findRoomName = s;
        Debug.Log("Successful finding name: " + findRoomName.text);
    }

    public void ReadCreateRoomName(string s)
    {
        //createRoomName = s;
        Debug.Log("Successful creation name: " + createRoomName.text);
    }*/
    
}
