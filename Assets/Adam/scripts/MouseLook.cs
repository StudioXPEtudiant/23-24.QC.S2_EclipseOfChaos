using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 2.5f; // Adjust sensitivity as needed

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the player's view horizontally based on mouse X movement
        transform.Rotate(Vector3.up * mouseX * sensitivity);

        // Rotate the camera vertically based on mouse Y movement
        float currentRotation = transform.localEulerAngles.x;
        float newRotation = Mathf.Clamp(currentRotation - mouseY * sensitivity, 0f, 90f);

        // Apply the new vertical rotation to the camera
        transform.localEulerAngles = new Vector3(newRotation, transform.localEulerAngles.y, 0f);
    }
}
