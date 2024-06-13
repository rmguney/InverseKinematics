using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 10, -10); 
    public float smoothSpeed = 0.125f;
    public Vector3 rotationAngle = new Vector3(45, 45, 0); 

    void Start()
    {
        transform.rotation = Quaternion.Euler(rotationAngle);
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        transform.rotation = Quaternion.Euler(rotationAngle);
    }
}
