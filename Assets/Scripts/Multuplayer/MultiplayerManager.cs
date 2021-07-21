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
    [SerializeField]
    Transform playerListContent;
    [SerializeField]
    GameObject playerListItemPrefab;

    [Header("Button")]
    [SerializeField]
    GameObject StartGameBtn;

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
    #region  ======================= Public : Start  =======================

    public void CreateRoom()
    {
        string roomName = "Room#";
        if (!string.IsNullOrEmpty(roomNameInput.text))
            roomName = roomNameInput.text;
        else
            roomName += Random.Range(0, 10000).ToString("0000");

        PhotonNetwork.CreateRoom(roomName);
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
    
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    #endregion  ======================= Public : end  =======================

    #region  ======================= Private : Start  =======================
    private GameObject CreatePlayer(Player player)
    {
        GameObject playerGo = Instantiate(playerListItemPrefab, playerListContent);
        playerGo.GetComponent<PlayerListItemController>().Init(player);
        return playerGo;
    }

    private void ClearPalyerRoomList()
    {
        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }
    }
    private void FillPlayerRoomList()
    {

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
            CreatePlayer(players[i]);

    }
    #endregion  ======================= Private : End  =======================

    #region  ======================= Photon Override : Start  =======================

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
        //En attendant la saisie du joueur
        PhotonNetwork.NickName = "Player#" + Random.Range(0, 10000).ToString("0000");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu(MenuType.Room);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        FillPlayerRoomList();

        StartGameBtn.SetActive(PhotonNetwork.IsMasterClient);
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
        ClearPalyerRoomList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        CreatePlayer(newPlayer);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform child in roomListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItemController>().Init(roomList[i]);
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        ClearPalyerRoomList();
        FillPlayerRoomList();

        StartGameBtn.SetActive(PhotonNetwork.IsMasterClient);
    }
    #endregion  ======================= Photon Override : End  =======================
}
