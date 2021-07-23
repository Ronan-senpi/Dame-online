using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnControlManager : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    private Photon.Realtime.Player basePlayer;

    private void Awake()
    {
        basePlayer = PhotonNetwork.PlayerList[playerIndex];
    }

    private void Start()
    {
        foreach(Transform t in transform)
        {
            PhotonView pv = t.GetComponent<PhotonView>();
            if (pv.Owner != basePlayer)
            {
                pv.TransferOwnership(basePlayer);

            }
        }
    }
}
