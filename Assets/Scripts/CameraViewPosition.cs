using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewPosition : MonoBehaviour
{
    public Transform mainCamera;
    public Transform[] views;
    private int currentview = 0;
    private int viewPosCount;

    void Start()
    {
        viewPosCount = views.Length;
        SetCameraPosition(0);
    }

    private void SetCameraPosition(int i)
    {
        mainCamera.position = views[i].position;
        mainCamera.rotation = views[i].rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetCameraPosition(++currentview % viewPosCount);
        }
    }
}
