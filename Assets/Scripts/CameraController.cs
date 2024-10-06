using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] framePrefabs;  // Array of 5 different frame prefabs
    public Transform playerStart;      // Target position to lerp to

    public GameObject levels;       // Container for the levels
    public GameObject cameraTrigger; // Trigger for the next frame
    public float playerLerpSpeed = 5f;  // The speed of the camera pan

    public float smoothSpeed = 0.25f;  // The speed of the camera pan
    public Vector3 offset;             // Offset for the camera position (relative to frames)

    public float xOffset = 20f;        // Distance between each frame on the x-axis
    private List<GameObject> frames = new List<GameObject>();  // List of spawned frames
    private Transform lastFrame;       // Keeps track of the last spawned frame

    private int currentFrameIndex = 0; // Tracks the current frame the camera is focusing on
    private int frameCloneIndex = 0;   // Tracks which frame prefab to clone next (loops 0 to 3)
    public int maxActiveFrames = 5;    // Max number of frames active at a time

    public int creaturesCollected;
    public int creaturesToCollect = 4;

    private void Start()
    {
        if (levels != null)
        {
            // Loop through all children of the Levels GameObject and destroy them
            foreach (Transform child in levels.transform)
            {
                Destroy(child.gameObject);
            }
        }
        // Spawn the initial 4 frames at the start
        for (int i = 0; i < 4; i++)
        {
            Vector3 newFramePosition = new Vector3(i * xOffset, 0, 0); // Spawns frames to the right
            GameObject frame = Instantiate(framePrefabs[i], newFramePosition, Quaternion.identity);
            frames.Add(frame);
            frame.transform.SetParent(levels.transform);
        }

        // Set the last frame reference to the first frame (start of the loop)
        lastFrame = frames[frames.Count - 1].transform;
    }

    private void LateUpdate()
    {
        // Ensure currentFrameIndex is within the valid range of the frames list
        currentFrameIndex = Mathf.Clamp(currentFrameIndex, 0, frames.Count - 1);

        // Move the camera smoothly to the current frame
        Vector3 desiredPosition = frames[currentFrameIndex].transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

        // This method can be used to manually move to the next frame (e.g., triggered by an event)
    public void MoveToNextFrame()
    {
        // Check if creaturesCollected equals the number needed to spawn Frame 5
        if (creaturesCollected == creaturesToCollect)
        {
            // Spawn Frame 5 (index 4 in the array)
            SpawnFrame(4);  // Frame 5 is at index 4
            creaturesCollected = 0;  // Reset creaturesCollected after spawning Frame 5
            
            // Ensure that the camera pans to the newly spawned Frame 5
            currentFrameIndex = frames.Count - 1;  
            cameraTrigger.active = false;
        }
        else
        {
            // Check if we've reached the last currently spawned frame
            if (currentFrameIndex == frames.Count - 1)
            {
                // Spawn the next frame in the sequence (looping through framePrefabs 1-4)
                SpawnFrame();
            }

            // Move to the next frame
            currentFrameIndex++;

            // Remove old frames that are no longer needed
            if (frames.Count > maxActiveFrames)
            {
                DestroyOldestFrame();
            }

            // Ensure that currentFrameIndex does not go out of bounds
            currentFrameIndex = Mathf.Clamp(currentFrameIndex, 0, frames.Count - 1);
            cameraTrigger.active = true;

        }
    }

    // SpawnFrame function to handle both specific and looped frame spawning
    private void SpawnFrame(int? specificFrameIndex = null)
    {
        // If a specific frame index is passed (e.g., Frame 5), use it; otherwise, use frameCloneIndex
        int prefabIndex = specificFrameIndex ?? frameCloneIndex;

        // Calculate the position of the new frame (20 units to the right of the last frame)
        Vector3 newFramePosition = new Vector3(lastFrame.position.x + xOffset, lastFrame.position.y, lastFrame.position.z);

        // Instantiate the specified frame prefab
        GameObject newFrame = Instantiate(framePrefabs[prefabIndex], newFramePosition, Quaternion.identity);

        // Add the new frame to the list and update the last frame reference
        frames.Add(newFrame);
        lastFrame = newFrame.transform;
        newFrame.transform.SetParent(levels.transform);


        // If we're not spawning a specific frame, loop through Frame 1 to Frame 4
        if (!specificFrameIndex.HasValue)
        {
            // Update the frameCloneIndex to loop between 0 and 3 (Frame 1 to Frame 4)
            frameCloneIndex = (frameCloneIndex + 1) % 4;
        }
    }

    private void DestroyOldestFrame()
    {
        // Destroy the oldest frame (first one in the list)
        GameObject oldestFrame = frames[0];
        frames.RemoveAt(0);  // Remove it from the list
        Destroy(oldestFrame);  // Destroy the GameObject
        currentFrameIndex--;    // Adjust the current frame index as the list has shrunk
    }


}
