using DitzelGames.FastIK;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 lookLimit;
    public Vector2 aimLimit;
    public Transform headTarget;
    public Transform pelvisTarget;
    public float pelvisMultiplier;
    public float pelvisAngleOffset;
    
    public float mouseSensitivity = 10;
    public float distanceFromTarget = 2;
    public float rotationSmoothTime;
    public float horizontalOffset;

    public FastIKFabric IKScript;
    public Vector3 currentAngle;
    public Vector3 targetAngle;
    public float IKSpeed;
    
    private Vector3 rotSmoothVel;
    private Vector3 currRotation;
    private float yaw;
    private float pitch;
    private float masterWeight;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        masterWeight = 0;
    }

    private void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, lookLimit.x, lookLimit.y);


        masterWeight = Mathf.Clamp(masterWeight, 0, 1);
        float dir = Input.GetMouseButton(0) ? 1 : -1;
        masterWeight += dir * 4 * Time.deltaTime;
        
        
        float headPitch = Mathf.InverseLerp(aimLimit.x, aimLimit.y, pitch) * 2 - pelvisAngleOffset;
        float pelvisPitch = pelvisAngleOffset - Mathf.InverseLerp(aimLimit.x, aimLimit.y, pitch) * 2;
        
        currRotation = Vector3.SmoothDamp(currRotation, new Vector3(pitch, yaw), ref rotSmoothVel, rotationSmoothTime);
        transform.eulerAngles = currRotation;
        if(headTarget)
            headTarget.transform.localPosition = new Vector3(0, 4.692f, headPitch);

        IKScript.enabled = Input.GetMouseButton(0);
        targetAngle = new Vector3(pelvisPitch * pelvisMultiplier, 0, 0);
        if (!IKScript.enabled)
            targetAngle = Vector3.zero;
        
        currentAngle = Vector3.MoveTowards(currentAngle, targetAngle, IKSpeed * Time.deltaTime);
        pelvisTarget.localEulerAngles = currentAngle;
        
        transform.position = target.position + (transform.right * horizontalOffset) - transform.forward * distanceFromTarget;
    }

}