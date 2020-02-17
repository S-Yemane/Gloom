using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 450f;   //Mouse Sensitivity
    public Transform playerBody;            //Assign playerBody via Unity Inspector
    float xRotation = 0f;                   //Initialization of variable
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Locks cursor to camera control and hides it.
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //multiplying by Time.deltaTime prevents the framerate/frequency Update() is called from
        //  affecting the mouse input

        //Vertical Camera Movement
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //Clamping locks the y-axis rotation from exceeding +-90 degrees
        //  In other words, Clamping prevents the camera from going upside down
        
        //Horizontal Camera Movement
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX); //Rotates the playerBody horizontally, NOT vertically
    }
}
