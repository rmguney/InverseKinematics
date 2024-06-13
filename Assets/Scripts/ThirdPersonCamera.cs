using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform playerTransform;
    public float mouseSensitivity = 100f;
    public float distanceFromPlayer = 2f;
    public float cameraHeight = 1.5f;

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -35f, 60f);

        yRotation += mouseX;

        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.position = playerTransform.position - (rotation * Vector3.forward * distanceFromPlayer) + (Vector3.up * cameraHeight);
        transform.LookAt(playerTransform.position + Vector3.up * cameraHeight);
    }
}
