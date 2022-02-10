using System;
using DitzelGames.FastIK;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 lookLimit;
    public float mouseSensitivity = 10;
    public float distanceFromTarget = 2;
    public float rotationSmoothTime;
    public float horizontalOffset;
    
    public float Pitch { get; private set; }
    public float Yaw { get; private set; }

    private Vector3 rotSmoothVel;
    private Vector3 currRotation;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        Pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        Pitch = Mathf.Clamp(Pitch, lookLimit.x, lookLimit.y);
        
        currRotation = Vector3.SmoothDamp(currRotation, new Vector3(Pitch, Yaw), ref rotSmoothVel,
            rotationSmoothTime);
        transform.eulerAngles = currRotation;

        transform.position = target.position + (transform.right * horizontalOffset) -
                             transform.forward * distanceFromTarget;
        
    }
}