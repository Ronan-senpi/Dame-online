using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RoomManager : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Rules parameters
    public bool separateControls { get; private set; }
    #endregion

    private static RoomManager instance;
    public static RoomManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<RoomManager>();
            return instance;
        }
        set { instance = value; }
    }

    public string leavingPlayer;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    private void Update()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            MenuManager.Instance.GetMenu(MenuType.Victory).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = leavingPlayer + " has left the game.";
            MenuManager.Instance.OpenMenu(MenuType.Victory);
        }
    }

    #region  ======================= Public : Start  =======================
    public void SetSeparationControlsState(bool state)
    {
        separateControls = state;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("Occurence called");
        if (stream.IsWriting)
        {
            stream.SendNext(separateControls);
            stream.SendNext(leavingPlayer);
        }
        else
        {
            separateControls = (bool)stream.ReceiveNext();
            leavingPlayer = (string)stream.ReceiveNext();
        }
    }

    public void OnPlayerLeave()
    {
        leavingPlayer = PhotonNetwork.NickName;
        StartCoroutine(LeaveWithDelay());
    }

    private IEnumerator LeaveWithDelay()
    {
        yield return new WaitForSeconds(3);
        PhotonNetwork.LeaveRoom();
#if UNITY_WEBPLAYER
     public static string webplayerQuitURL = "http://ronan-dhersignerie.fr/";
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
    }


    #endregion ======================= Public : Start  =======================

    #region  ======================= Private : Start  =======================
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1) //if game secene 
        {
            //Instantiate RoomMangar at (0,0,0) because it's a Empty
            PhotonNetwork.Instantiate(Path.Combine(MultiplayerManager.PhotonPrefabPath, "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }
    #endregion ======================= Private : Start  =======================
}
