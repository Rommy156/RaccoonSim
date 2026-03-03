
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyeballs : MonoBehaviour
{
    //variable for movement of mouse speed
    public float sensitivity = 5f;
    //variable to smooth out camera directional movement 
    public float smoothFactor = 1.5f;
    //variables to store calculations for us, they are not visible in unity inspector 
    //variable that holds mouse direction calculations
    private Vector2 mouseLook;
    //variable that holds smooth movement calculations
    private Vector2 smoothMove;
    //get refrence to the player
    public GameObject playerRef;

    // Start is called before the first frame update
    void Start()
    {
        //the camera is the child of player in unity so we can assign using dot operator.
        playerRef = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //make cursor invisible.
        //ESC to get cursor back
        Cursor.lockState = CursorLockMode.Locked;
        //lets create a temporary variable to store movement
        Vector2 mouseDirection = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //scale the x and y of the sensitivity and smoothFactor variables 
        mouseDirection.x *= sensitivity * smoothFactor;
        mouseDirection.y *= sensitivity * smoothFactor;
        //linear interpolation [lerp] between current position, calculated position at a speed of 1/ smoothFactor
        //normalize x and y movement, direction, 
        smoothMove.x = Mathf.Lerp(smoothMove.x, mouseDirection.x, 1f / smoothFactor);
        smoothMove.y = Mathf.Lerp(smoothMove.y, mouseDirection.y, 1f / smoothFactor);
        //add those two calculations together
        mouseLook += smoothMove;
        //clamp to ensure mouse movement is limited
        //Clamp parameters min,max 
        mouseLook.y = Mathf.Clamp(mouseLook.y, -85f, 95f);
        //rotate camera on newly calculated position
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        //player moves on the x axis only
        playerRef.transform.rotation = Quaternion.AngleAxis(mouseLook.x, playerRef.transform.up);


    }
}
