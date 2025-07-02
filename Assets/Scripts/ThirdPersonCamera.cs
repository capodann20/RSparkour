using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float sensitivity = 2f;
    public float distance = 5f;
    float currentX = 0f;
    float currentY = 0f;
    public float yMin = -20f;
    public float yMax = 60f;

    void LateUpdate()
    {
        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;
        currentY = Mathf.Clamp(currentY, yMin, yMax);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 desiredPosition = target.position + rotation * new Vector3(0, 0, -distance) + offset;

        transform.position = desiredPosition;
        transform.LookAt(target.position + offset);
    }
}
