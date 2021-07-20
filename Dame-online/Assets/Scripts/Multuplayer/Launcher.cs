using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Launcher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {

        MenuManager.instance.OpenMenu("Loading");
        Debug.Log("connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {

        Debug.Log("connected to Master");

        Debug.Log("Joining lobby");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        MenuManager.instance.OpenMenu("Title");
        Debug.Log("Joined lobby");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
