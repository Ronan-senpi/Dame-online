using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 5f;
    [SerializeField]
    float smoothFactor = 5f;

    Vector3 offsetCam;
    private void Start()
    {
        offsetCam = Camera.main.transform.position - Vector3.zero;
        Camera.main.transform.LookAt(Vector3.zero);
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            offsetCam = camTurnAngle * offsetCam;
            Camera.main.transform.position = Vector3.Slerp(Camera.main.transform.position, offsetCam, smoothFactor);
            Camera.main.transform.LookAt(Vector3.zero);
        }
    }
}
