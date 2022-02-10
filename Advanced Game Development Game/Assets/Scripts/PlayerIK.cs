using System;
using DitzelGames.FastIK;
using UnityEngine;

public class PlayerIK : MonoBehaviour
{
    public FastIKFabric spineIKScript;
    public Transform pelvisTarget;
    public Transform headTarget;
    
    public Vector2 aimLimit;
    public Vector2 headLimit;
    public float pelvisMultiplier = -38;
    public float rotateSpeed = 80;
    public float angle;
    
    private Vector3 m_PelvisTargetAngle;
    private Vector3 m_HeadTargetAngle;
    private Vector3 m_CurrPelvisAngle;
    private Vector3 _CurrHeadAngle;
    private CameraFollow m_CamFollow;
    private float clampedYaw;
    private Quaternion center;

    private float Yaw;
    private float Pitch;
    private void Start()
    {
        m_CamFollow = FindObjectOfType<CameraFollow>();
    }

    private void Update()
    {
        float spinePitch = 0.42f - Mathf.InverseLerp(aimLimit.x, aimLimit.y, m_CamFollow.Pitch) * 2;
        float headPitch = Mathf.InverseLerp(headLimit.x, headLimit.y, m_CamFollow.Pitch) * 2;
        float camY = m_CamFollow.Yaw / 2; 
        m_PelvisTargetAngle = new Vector3(spinePitch * pelvisMultiplier, 0, 0);
        
        float yawClamp = Mathf.Clamp(m_CamFollow.Yaw, -44, 44);
        float pitchClamp = Mathf.Clamp(m_CamFollow.Pitch / 1.5f, -19, 24);
        
            
        m_CurrPelvisAngle = Vector3.MoveTowards(m_CurrPelvisAngle, m_PelvisTargetAngle, rotateSpeed * Time.deltaTime);

        Vector3 fixedCamForward = Camera.main.transform.forward;
        fixedCamForward.y = 0;
        
        angle = Vector3.SignedAngle(transform.forward, fixedCamForward, Vector3.up);
        m_HeadTargetAngle = transform.up * angle;
        m_HeadTargetAngle.y = Mathf.Clamp(m_HeadTargetAngle.y, -35, 35);

        _CurrHeadAngle = Vector3.MoveTowards(_CurrHeadAngle, m_HeadTargetAngle,
            rotateSpeed * Time.deltaTime);
        headTarget.localEulerAngles = _CurrHeadAngle;
        pelvisTarget.localEulerAngles = m_CurrPelvisAngle;
        
        _CurrHeadAngle = Vector3.MoveTowards(_CurrHeadAngle, m_HeadTargetAngle, rotateSpeed * Time.deltaTime);

        if (!spineIKScript.enabled)
            m_PelvisTargetAngle = Vector3.zero;
        
        Yaw += Input.GetAxis("Mouse X") * m_CamFollow.mouseSensitivity;
        Pitch -= Input.GetAxis("Mouse Y") * m_CamFollow.mouseSensitivity;

        Vector3 target = (transform.forward + Camera.main.transform.forward).normalized;
        target.y = 0;



    }

    
}
