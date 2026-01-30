using UnityEngine;
public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    [Header("Camera Settings")]

    public float distance = 5f;
    public float height = 2f;
    public float sensitivity = 3f;

    [Header("Vertical Clamp")]

    public float minY = -30f;
    public float maxY = 60f;
    private float mouseX;
    private float mouseY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * sensitivity;

        mouseY -= Input.GetAxis("Mouse Y") * sensitivity;
        mouseY = Mathf.Clamp(mouseY, minY, maxY);
    }
    void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0f);
        Vector3 position =
            target.position

            - (rotation * Vector3.forward * distance)
            + Vector3.up * height;
        transform.position = position;
        transform.LookAt(target.position + Vector3.up * height);
    }
}