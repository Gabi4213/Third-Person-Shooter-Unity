using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorScreen : MonoBehaviour
{
    // Reference to the camera rendering the scene
    public Camera mainCamera;

    // Flag to track if the camera view is mirrored or not
    private bool isMirrored = false;

    void Update()
    {
        // Check if 'F' key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Toggle the mirror effect
            ToggleMirror();
        }
    }

    // Function to toggle the mirror effect
    void ToggleMirror()
    {
        // Toggle the flag
        isMirrored = !isMirrored;

        // If the camera view is mirrored, flip it
        if (isMirrored)
        {
            // Mirror the camera view by flipping its projection matrix
            Matrix4x4 mat = mainCamera.projectionMatrix;
            mat *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
            mainCamera.projectionMatrix = mat;
        }
        else
        {
            // Reset the camera view to its original state
            mainCamera.ResetProjectionMatrix();
        }
    }
}
