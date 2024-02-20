using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The car's transform to follow
    public Vector3 offset;   // Offset from the car

    public float smoothSpeed = 0.125f; // Speed at which the camera follows the car

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not set for CameraFollow script.");
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
