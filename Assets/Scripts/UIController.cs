using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Import the Scene Management namespace


public class UIController : MonoBehaviour
{

    public int currentDM;
    public CameraController mainCam;

    // Update is called once per frame
    void Update()
    {
        mainCam.currentDM = currentDM;

        // Detect when the R key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
         // Detect when the 1 key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentDM = 0;
        }
        // Detect when the 1 key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentDM = 1;
        }
        // Detect when the 1 key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentDM = 2;
        }
        // Detect when the 1 key is pressed
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentDM = 3;
        }

    }

      // Method to reload the current scene
    void ReloadScene()
    {
        // Get the current active scene and reload it
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
