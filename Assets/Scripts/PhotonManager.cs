using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject[] SpawnPoints;

    [SerializeField]
    TextMeshProUGUI P1Score;

    [SerializeField]
    TextMeshProUGUI P2Score;

    [SerializeField]
    TextMeshProUGUI P3Score;

    [SerializeField]
    TextMeshProUGUI P4Score;

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        int player = 0;
        if (!PhotonNetwork.IsMasterClient)
        {
            player = 1;
        }
        GameObject Player = PhotonNetwork.Instantiate("Player", SpawnPoints[player].transform.position, Quaternion.identity);
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        SetScoreText();
    }

    private void SetScoreText()
    {
        P1Score.text = "P1 Score: " + PhotonNetwork.CurrentRoom.CustomProperties["P1SCORE"].ToString();
        P2Score.text = "P2 Score: " + PhotonNetwork.CurrentRoom.CustomProperties["P2SCORE"].ToString();
        P3Score.text = "P3 Score: " + PhotonNetwork.CurrentRoom.CustomProperties["P3SCORE"].ToString();
        P4Score.text = "P4 Score: " + PhotonNetwork.CurrentRoom.CustomProperties["P4SCORE"].ToString();
    }
}
