using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera[] cameras;
    private int currentCameraIndex;

    private void Start()
    {
        // Disable all cameras except the first one
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        // Set the first camera as the current camera
        currentCameraIndex = 0;
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }

    private void Update()
    {
        // Check for input to switch the camera view
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Disable the current camera
            cameras[currentCameraIndex].gameObject.SetActive(false);

            // Move to the next camera in the array
            currentCameraIndex++;
            if (currentCameraIndex >= cameras.Length)
            {
                currentCameraIndex = 0;
            }

            // Enable the new current camera
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }
}
