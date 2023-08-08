using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ColorManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Color[] PlayerColors = { Color.blue, Color.red, Color.green, Color.yellow };

    void Start()
    {
        int playerID = PhotonNetwork.LocalPlayer.ActorNumber;
        Vector3 vColor;
        switch (playerID) {
            case 1:
                vColor = new Vector3 (0f, 0f, 255f);
                break;
            case 2:
                vColor = new Vector3(255f, 0f, 0f);
                break;
            case 3:
                vColor = new Vector3(0f, 255f, 0f);
                break;
            case 4:
                vColor = new Vector3(125f, 125f, 0f);
                break;
            default:
                vColor = new Vector3(255f, 255f, 255f);
                break;
        }
        /*if (photonView.IsMine)
        {
            photonView.RPC("SetColor", RpcTarget.AllBuffered, vColor);
        }*/
        photonView.RPC("SetColor", RpcTarget.AllBuffered, vColor);
    }

    [PunRPC]
    public void SetColor(Vector3 color)
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color (color.x/255, color.y/255, color.z/255);
    }
}
