using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DragableObject : MonoBehaviourPun, IPunObservable
{
    private Vector3 mOffset;
    private float mZCoord;
    private PhotonView pv;
    private Rigidbody rb;
    private Photon.Realtime.Player baseId;
    private bool canControl = true;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        baseId = pv.Owner;
    }

    private void Start()
    {
        if (!TryGetComponent(out rb))
        {
            throw new Exception("Cet objet (DragableObject) a besoin d'un Rigidbody");
        }
    }
    private void OnMouseDown()
    {
        if (pv.Owner != PhotonNetwork.LocalPlayer)
        {
            if (!RoomManager.Instance.separateControls)
            {
                pv.TransferOwnership(PhotonNetwork.LocalPlayer);
                canControl = true;
            }
            else
            {
                canControl = false;
            }
        }
        if (canControl)
        {
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
            rb.isKinematic = true;
        }
    }

    void OnMouseDrag()
    {
        if (canControl)
        {
            Vector3 nPos = GetMouseWorldPos() + mOffset;
            if (nPos.y < 0.45f)
            {
                nPos.y = 0.45f;
            }
            transform.position = nPos;
        }
    }

    private void OnMouseUp()
    {
        rb.isKinematic = false;
    }


    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rb.isKinematic);
        }
        else
        {
            rb.isKinematic = (bool)stream.ReceiveNext();
        }
    }
}
