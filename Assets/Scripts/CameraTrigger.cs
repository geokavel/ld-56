using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    private CameraController cameraController;
    public CameraController[] DMCameras;

    private void Start()
    {
        // Reference the camera controller (ensure the camera has this script attached)
        cameraController = Camera.main.GetComponent<CameraController>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // When the player enters the trigger, move the camera to the next frame
        if (other.CompareTag("Player"))
        {
            cameraController.MoveToNextFrame();

            for (int i = 0; i < DMCameras.Length; i++)     
            {
                DMCameras[i].MoveToNextFrame();
            }       
        }
    }   
}
