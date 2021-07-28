using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnControlManager : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    private Photon.Realtime.Player basePlayer;

    private void Awake()
    {
        try
        {
            basePlayer = PhotonNetwork.PlayerList[playerIndex];
        }
        catch (Exception e)
        {

        }
    }

    private void Start()
    {
        foreach (Transform t in transform)
        {
            PhotonView pv = t.GetComponent<PhotonView>();
            if (pv.Owner != basePlayer)
            {
                pv.TransferOwnership(basePlayer);

            }
        }
    }
}
