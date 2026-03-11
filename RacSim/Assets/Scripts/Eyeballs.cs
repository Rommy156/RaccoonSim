//Allen Adepoju
//000948096
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyeballs : MonoBehaviour
{
    public float sensitivity = 3f;
    public float smoothFactor = 5f;
    private Vector2 mouseLook;
    private Vector2 smoothMove;
    public GameObject playerRef;

    void Start()
    {
        playerRef = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        

        Vector2 mouseDirection = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        mouseDirection *= sensitivity;
        

        smoothMove.x = Mathf.Lerp(smoothMove.x, mouseDirection.x, 1f / smoothFactor);
        smoothMove.y = Mathf.Lerp(smoothMove.y, mouseDirection.y, 1f / smoothFactor);

        mouseLook += smoothMove;
        mouseLook.y = Mathf.Clamp(mouseLook.y, -85f, 85f);
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        playerRef.transform.Rotate(Vector3.up * smoothMove.x);


    }
}
