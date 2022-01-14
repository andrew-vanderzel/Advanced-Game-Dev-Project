using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector2 aimLimit;
    public Transform headTarget;
    public Transform pelvisTarget;
    public float pelvisAngleOffset;
    
    public float mouseSensitivity = 10;
    public float distanceFromTarget = 2;
    public float rotationSmoothTime;
    public float horizontalOffset;
    
    private Vector3 rotSmoothVel;
    private Vector3 currRotation;
    private float yaw;
    private float pitch;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -5, 45);
        float headPitch = Mathf.InverseLerp(aimLimit.x, aimLimit.y, pitch) * 2 - pelvisAngleOffset;
        float pelvisPitch = pelvisAngleOffset - Mathf.InverseLerp(aimLimit.x, aimLimit.y, pitch) * 2;
        
        currRotation = Vector3.SmoothDamp(currRotation, new Vector3(pitch, yaw), ref rotSmoothVel, rotationSmoothTime);
        transform.eulerAngles = currRotation;

        headTarget.transform.localPosition = new Vector3(0, 3.692f, headPitch);
        pelvisTarget.localPosition = new Vector3(0, pelvisPitch + 0.824f, 1);
        transform.position = target.position + (transform.right * horizontalOffset) - transform.forward * distanceFromTarget;
    }

}