using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableObject : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    private Rigidbody rb;

    private void Start()
    {
        if(!TryGetComponent(out rb))
        {
            throw new Exception("Cet objet (DragableObject) a besoin d'un Rigidbody");
        }
    }
    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        rb.isKinematic = true;
    }

    void OnMouseDrag()
    {
        Vector3 nPos = GetMouseWorldPos() + mOffset;
        if (nPos.y < 0.45f)
        {
            nPos.y = 0.45f;
        }
        transform.position = nPos;
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

}
