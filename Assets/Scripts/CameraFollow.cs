using UnityEngine;

public class ThirdPersonCameraFollow : MonoBehaviour
{
    public Transform carTransform; // Reference to the car's transform
    public Vector3 offset; // Offset of the camera position
    public float smoothSpeed = 0.125f; // Adjust for smoother movement

    void LateUpdate()
    {
        // Calculate the desired position
        Vector3 desiredPosition = carTransform.position - carTransform.forward * offset.z + carTransform.up * offset.y;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Align the camera's rotation with the car's rotation
        Quaternion desiredRotation = Quaternion.LookRotation(carTransform.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed);
    }
}
