using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHover : MonoBehaviour
{
    public float hoverSlowDown;
    private PlayerMovement pMovement;
    private Rigidbody rb;
    

    private void Start()
    {
        pMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Hover();
    }

    public void Hover()
    {
        if (Input.GetKey(KeyCode.Space) && rb.velocity.y < 0 && !GroundCheck())
        {
            
            pMovement.SetGravityOverride(hoverSlowDown);
        }
    }
    
    private bool GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.2f))
        {
            return true;
        }
        
        return false;
    }
    
}
