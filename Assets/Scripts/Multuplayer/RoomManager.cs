using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class RoomManager : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Rules parameters
    public bool separateControls;
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

    #region  ======================= Public : Start  =======================
    public void SetSeparationControlsState(bool state)
    {
        separateControls = state;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(separateControls);
        }
        else
        {
            separateControls = (bool)stream.ReceiveNext();
        }
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
