using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ScoreManager : MonoBehaviourPun
{
    private int score = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Point")
        {
            if (photonView.IsMine)
            {
                score++;

                if(PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "P1SCORE", score } });
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
                {
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "P2SCORE", score } });
                }
                else if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
                {
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "P3SCORE", score } });
                }
                else
                {
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "P4SCORE", score } });
                }
            }
        }


    }

    private void OnDestroy()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(0);
        }
    }
}
