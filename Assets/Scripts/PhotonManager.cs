using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Unity.Netcode;
using Photon.Realtime;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;
using Photon.Pun.UtilityScripts;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject[] SpawnPoints;

    [SerializeField]
    Color[] PlayerColors;

    [SerializeField]
    TextMeshProUGUI P1Score;

    [SerializeField]
    TextMeshProUGUI P2Score;

    [SerializeField]
    TextMeshProUGUI P3Score;

    [SerializeField]
    TextMeshProUGUI P4Score;

    [SerializeField]
    TextMeshProUGUI textID;

    [SerializeField]
    TextMeshProUGUI remainingText;

    [SerializeField]
    GameObject GameEndPanel;

    [SerializeField]
    GameObject _player;

    [SerializeField]
    TextMeshProUGUI endText;

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }

    }

    private void SpawnPlayer()
    {
        int playerID;
        playerID = PhotonNetwork.LocalPlayer.ActorNumber;

        GameObject Player = PhotonNetwork.Instantiate("Player", SpawnPoints[playerID - 1].transform.position, Quaternion.identity);
        textID.text = "Player ID: " + playerID.ToString();
        _player = Player;
        int remaining = PhotonNetwork.CurrentRoom.PlayerCount;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "REMAIN", remaining } });
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        SetScoreText();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateRemainingCount();
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Time.timeScale = 0f;
            GameEndPanel.SetActive(true);
            int num = PhotonNetwork.LocalPlayer.ActorNumber;
            endText.text = "Player " + num.ToString() + " has won! His coins: " + PhotonNetwork.CurrentRoom.CustomProperties["P" + num.ToString() + "SCORE"].ToString();
        }
    }

    private void SetScoreText()
    {
        P1Score.text = "P1 Score: " + PhotonNetwork.CurrentRoom.CustomProperties["P1SCORE"].ToString();
        P2Score.text = "P2 Score: " + PhotonNetwork.CurrentRoom.CustomProperties["P2SCORE"].ToString();
        P3Score.text = "P3 Score: " + PhotonNetwork.CurrentRoom.CustomProperties["P3SCORE"].ToString();
        P4Score.text = "P4 Score: " + PhotonNetwork.CurrentRoom.CustomProperties["P4SCORE"].ToString();
    }

    private void UpdateRemainingCount()
    {
        remainingText.text = "Remaining: " + PhotonNetwork.CurrentRoom.CustomProperties["REMAIN"].ToString();
    }
}
