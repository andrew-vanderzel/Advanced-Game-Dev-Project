using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float turnSmoothTime = 0.2f;
    
    private float groundDistance = 1.6f;
    private float _overrideTime;
    private Vector3 _movementOverride;
    private float _tsv;
    private Rigidbody _rb;
    private Transform _cameraTransform;
    private PlayerStats _stats;

    public Vector3 dir;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform;
        _stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (_overrideTime <= 0)
            _movementOverride = Vector3.zero;
        else
            _overrideTime -= 1 * Time.deltaTime;

        dir = _movementOverride == Vector3.zero ? MoveInput() : _movementOverride;
        
        Jump();
    }

    private void FixedUpdate()
    {
        if(!_stats.IsDead())
            _rb.MovePosition(transform.position + dir * movementSpeed * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        
        if (Input.GetAxis("Jump") > 0 && GroundCheck()) 
        {
            _rb.velocity = new Vector3(_rb.velocity.x, 5, _rb.velocity.z);
        }
    }

    public Vector3 MoveInput()
    {
        Vector2 input = new Vector2
        {
            x = Input.GetAxis("Horizontal"),
            y = Input.GetAxis("Vertical")
        }.normalized;
        
        if (input != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _tsv, turnSmoothTime);
        }
        
        return (transform.forward * input.magnitude).normalized;
    }
    
    private bool GroundCheck()
    {
        return Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, groundDistance);
    }

    public void SetMovementOverride(Vector3 dir, float time)
    {
        _overrideTime = time;
        _movementOverride = dir;
    }
}