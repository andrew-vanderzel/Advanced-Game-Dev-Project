using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float turnSmoothTime = 0.2f;
    public float groundDistance;
    public bool onGround;
    public Vector3 movementOverride;
    public float gravityOverride;
    
    public float overrideTime;
    
    private float tsv;
    private Rigidbody rb;
    private Transform t;
    private PlayerStats stats;

    private Vector3 dir;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = Camera.main.transform;
        stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (overrideTime <= 0)
            movementOverride = Vector3.zero;
        else
            overrideTime -= 1 * Time.deltaTime;
        
        Move();
    }

    private void FixedUpdate()
    {
        if(!stats.IsDead())
            rb.MovePosition(transform.position + dir * movementSpeed * Time.deltaTime);
        
        Jump();
    }


    private void Jump()
    {
        onGround = GroundCheck();
        if (Input.GetAxis("Jump") > 0 && GroundCheck())
        {
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
        }
    }

    private void Move()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input.Normalize();
        
        if (input != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + t.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref tsv, turnSmoothTime);
        }

        dir = (transform.forward * input.magnitude).normalized;
    }
    
    private bool GroundCheck()
    {
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, groundDistance))
        {
            return true;
        }
        
        return false;
    }

    public void SetMovementOverride(Vector3 dir, float time)
    {
        overrideTime = time;
        movementOverride = dir;
    }

    public void SetGravityOverride(float val)
    {
        rb.velocity = new Vector3(rb.velocity.x, val, rb.velocity.z);
    }


}
