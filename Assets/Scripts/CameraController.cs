using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float horizontalResolution = Screen.width; 

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 2;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame 
    // TODO: Fix with a better code. -- Unnecessary code and conditions 
    void Update()
    {
            QualitySettings.vSyncCount = 2;
            Application.targetFrameRate = 60;       

            // Screen.SetResolution(1920, 1080, true, 60);
            // horizontalResolution = Screen.width;

            // float screenRatio = (float)1920 / (float)1080;
            // float targetRatio = (float)1920 / (float)1080;

            // if (screenRatio >= targetRatio)
            // {
            //     Camera.main.orthographicSize = (float)((float)1080 / 2);
            // }
            // else
            // {
            //     float differenceInSize = targetRatio / screenRatio;
            //     Camera.main.orthographicSize = (float)((float)1080 / 2 * differenceInSize);
            // }


    }
}
