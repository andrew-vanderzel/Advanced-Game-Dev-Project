using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySaw : Enemy
{
    public float sawSpeed;
    public float wheelSpeed;
    public Transform saw;
    public Transform wheel;
    public float turnDifference;
    public float tiltSpeed;
    public float currentTilt;
    public float tiltMultiplier;
    public Transform tiltPivot;
    public bool toInspect;

    private float _previousY;
    private float _currentY;
    
    private new void Start()
    {
        base.Start();
        eAgent = GetComponent<NavMeshAgent>();

        _previousY = transform.eulerAngles.y;
        _currentY = _previousY;
    }

    protected override void StandardMovement()
    {
        saw.localEulerAngles += Vector3.forward * sawSpeed * Time.deltaTime;
        wheel.eulerAngles += Vector3.forward * wheelSpeed * eAgent.velocity.magnitude * Time.deltaTime;
        
        _currentY = transform.eulerAngles.y;
        turnDifference = _currentY - _previousY;
        currentTilt = Mathf.MoveTowards(currentTilt, turnDifference, tiltSpeed * Time.deltaTime);
        TiltPivot();
    }

    protected override void SpecificDeath()
    {
        sawSpeed = Mathf.MoveTowards(sawSpeed, 0, 140 * Time.deltaTime);
        
        if(eAgent.speed <= 0)
            eAgent.enabled = false;
        else
            eAgent.speed -= 0.8f * Time.deltaTime;
    }

    protected override void AttackBehavior()
    {
        eAgent.destination = target.position;
    }

    private void TiltPivot()
    {
        tiltPivot.localEulerAngles = new Vector3(0, 0, currentTilt * -tiltMultiplier);
    }

    private void LateUpdate()
    {
        _previousY = _currentY;
    }
}