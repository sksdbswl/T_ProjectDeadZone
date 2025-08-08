using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform cameraRoot;  // 좌우 회전 (Y축)
    [SerializeField] private Transform cameraPivot; // 상하 회전 (X축)

    [SerializeField] private float mouseSensitivity = 3f;
    [SerializeField] private float clampAngle = 80f;

    private float xRotation = 0f;

    // void Update()
    // {
    //     float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
    //     float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
    //
    //     // 좌우 회전 (Y축)
    //     cameraRoot.Rotate(Vector3.up * mouseX);
    //
    //     // 상하 회전 (X축, 제한 필요)
    //     xRotation -= mouseY;
    //     xRotation = Mathf.Clamp(xRotation, -clampAngle, clampAngle);
    //     cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    // }
}