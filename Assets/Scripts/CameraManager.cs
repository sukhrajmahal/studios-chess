using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        RotateCamera(2);
    }

    private void RotateCamera(int angle)
    {
        Vector3 position = new Vector3(4f, 4.35f, -1.49f);
        Quaternion rotation = new Quaternion();
        if (angle == 0)
        {
            position = new Vector3(4f, 4.35f, -1.49f);
            rotation.eulerAngles = new Vector3(45f, 0f, 0f);
        }
        else if (angle == 1)
        {
            position = new Vector3(-1.49f, 4.35f, 4f);
            rotation.eulerAngles = new Vector3(45f, 90f, 0f);
        }
        else if (angle == 2)
        {
            position = new Vector3(4f, 4.35f, 9.57f);
            rotation.eulerAngles = new Vector3(45f, 180f, 0f);
        }
        else
        {
            position = new Vector3(9.57f, 4.35f, 4f);
            rotation.eulerAngles = new Vector3(45f, -90f, 0f);
        }
        mainCamera.transform.position = position;
        mainCamera.transform.rotation = rotation;
    }
}
