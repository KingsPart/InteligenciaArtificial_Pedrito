using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;

    [SerializeField] private float sensitivity = 100f;

    [SerializeField] private float minPitch = -40f;
    [SerializeField] private float maxPitch = 70f;

    private float yaw;
    private float pitch;

    public void Look(Vector2 lookInput)
    {
        yaw += lookInput.x * sensitivity * Time.deltaTime;
        pitch -= lookInput.y * sensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        cameraTarget.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
