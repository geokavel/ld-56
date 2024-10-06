using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] frames;  // Array of positions the camera will move between (each frame of the level)
    public Transform playerStart;   // Target position to lerp to
    public Rigidbody2D playerRB;     // Player Rigidbody
    public float smoothSpeed = 0.00125f;  // The speed of the camera pan
    public float playerLerpSpeed = 5f;  // Speed of the movement
    public Vector3 offset;  // Offset for the camera position (relative to frames)

    private int currentFrameIndex = 0;  // Tracks the current frame the camera is focusing on

    private bool panning;

    private void FixedUpdate(){
    //    LerpPlayer();
    }

    private void LateUpdate()
    {
        // Ensure the camera follows the current frame position smoothly
        Vector3 desiredPosition = frames[currentFrameIndex].position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }

    public void MoveToNextFrame()
    {
        // Move to the next frame when called
        if (currentFrameIndex < frames.Length - 1)
        {
            currentFrameIndex++;
            LerpPlayer();
            
        }
    }

    private void LerpPlayer()
    {
        Vector3 smoothedPosition = Vector3.Lerp(playerRB.position, playerStart.position, playerLerpSpeed * Time.fixedDeltaTime);
        playerRB.MovePosition(smoothedPosition);

    }

}
