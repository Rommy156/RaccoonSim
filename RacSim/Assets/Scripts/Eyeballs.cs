
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyeballs : MonoBehaviour
{
    public float sensitivity = 5f;
    public float smoothFactor = 1.5f;
    private Vector2 mouseLook;
    private Vector2 smoothMove;
    public GameObject playerRef;

    void Start()
    {
        playerRef = transform.parent.gameObject;
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Vector2 mouseDirection = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        mouseDirection.x *= sensitivity * smoothFactor;
        mouseDirection.y *= sensitivity * smoothFactor;

        smoothMove.x = Mathf.Lerp(smoothMove.x, mouseDirection.x, 1f / smoothFactor);
        smoothMove.y = Mathf.Lerp(smoothMove.y, mouseDirection.y, 1f / smoothFactor);

        mouseLook += smoothMove;
        mouseLook.y = Mathf.Clamp(mouseLook.y, -85f, 95f);
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        playerRef.transform.rotation = Quaternion.AngleAxis(mouseLook.x, playerRef.transform.up);


    }
}
