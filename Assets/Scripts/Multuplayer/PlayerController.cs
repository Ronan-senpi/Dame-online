using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("I am master client");
        }
        else
        {
            Debug.Log("I am not master client");
        }
    }
}
