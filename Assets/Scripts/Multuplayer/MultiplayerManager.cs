using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    public static MultiplayerManager Instance;
    [Header("Inputs")]
    [SerializeField]
    TMP_InputField roomNameInput;

    [Header("Text")]
    [SerializeField]
    TMP_Text errorText;
    [SerializeField]
    TMP_Text roomNameText;

    [Header("Containers / Entities")]
    [SerializeField]
    Transform roomListContent;
    [SerializeField]
    GameObject roomListItemPrefab;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        MenuManager.Instance.OpenMenu(MenuType.Loading);
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInput.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInput.text);
        MenuManager.Instance.OpenMenu(MenuType.Loading);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu(MenuType.Loading);
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu(MenuType.Loading);
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomNameInput.text);
        MenuManager.Instance.OpenMenu(MenuType.Loading);
    }

    #region Photon Override : Start

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu(MenuType.Title);
        Debug.Log("Joined Lobby");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu(MenuType.Room);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        Debug.LogError("Room Creation Failed: " + message);
        MenuManager.Instance.OpenMenu(MenuType.Error);
    }


    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu(MenuType.Title);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItemController>().Init(roomList[i]);
        }
    }
    #endregion Photon Override : End
}
