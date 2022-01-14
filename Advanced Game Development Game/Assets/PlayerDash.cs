using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashStrength;
    public float dashTime;
    public Vector3 rawMovement;
    private PlayerMovement pMovement;

    private void Start()
    {
        pMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }

    public void Dash()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = input.normalized;
        Vector3 targVel = transform.forward;
        
        targVel *= dashStrength; 
        pMovement.SetMovementOverride(targVel, dashTime);
    }
}
