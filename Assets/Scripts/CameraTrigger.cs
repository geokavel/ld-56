using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    private CameraController cameraController;
    public CameraController[] DMCameras;

    public GameObject mainLevelFrame;

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

         /*
            // Try to find the "CreatureSpawnPoint" in the current frame
            Transform creatureSpawnPoint = mainLevelFrame.transform.Find("CreatureSpawnPoint");

            if (creatureSpawnPoint != null)
            {
                // Set the player's spawn point to the creature spawn point in the current frame
                PlayerController playerScript = other.GetComponent<PlayerController>();
                if (playerScript != null)
                {
                   // playerScript.spawnPoint = creatureSpawnPoint;
                    Debug.Log("CreatureSpawnPoint not found in the current frame.");

                }
            }
            else
            {
                Debug.LogError("CreatureSpawnPoint not found in the current frame.");
            }   */


            for (int i = 0; i < DMCameras.Length; i++)     
            {
                DMCameras[i].MoveToNextFrame();
            }       
        }
    }   
}
