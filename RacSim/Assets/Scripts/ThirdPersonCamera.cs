using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Target to follow (usually the Player)
    [Header("Camera Settings")]
    public float distance = 5f; // Distance from the target
    public float height = 2f; // Height offset from the target
    public float sensitivity = 3f; // Mouse sensitivity

    [Header("Vertical Clamp")]
    public float minY = -30f; // Minimum vertical rotation (up/down)
    public float maxY = 60f;  // Maximum vertical rotation (up/down)
    private float mouseX; // Horizontal mouse input
    private float mouseY; // Vertical mouse input

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
    }

    void Update()
    {
        // Get the mouse input
        mouseX += Input.GetAxis("Mouse X") * sensitivity; // Horizontal rotation
        mouseY -= Input.GetAxis("Mouse Y") * sensitivity; // Vertical rotation
        mouseY = Mathf.Clamp(mouseY, minY, maxY); // Clamp vertical rotation
    }

    void LateUpdate()
    {
        // Apply the camera rotation based on mouse movement
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0f);

        // Calculate the desired camera position based on the rotation and offset
        Vector3 position = target.position - (rotation * Vector3.forward * distance) + Vector3.up * height;

        // Set the camera position
        transform.position = position;

        // Make the camera look at the target (player)
        transform.LookAt(target.position + Vector3.up * height);
    }
}