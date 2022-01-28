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
    private NavMeshAgent eAgent;

    private float previousY;
    private float currentY;
    
    private new void Start()
    {
        base.Start();
        eAgent = GetComponent<NavMeshAgent>();

        previousY = transform.eulerAngles.y;
        currentY = previousY;
    }

    private new void Update()
    {
        base.Update();
         
        saw.localEulerAngles += Vector3.forward * sawSpeed * Time.deltaTime;
        wheel.eulerAngles += Vector3.forward * wheelSpeed * eAgent.velocity.magnitude * Time.deltaTime;
        
        if (stats.health <= 0)
        {
            sawSpeed = Mathf.MoveTowards(sawSpeed, 0, 140 * Time.deltaTime);
            
            if(eAgent.speed <= 0)
                eAgent.enabled = false;
            else
                eAgent.speed -= 0.8f * Time.deltaTime;
            return;
        }
        
        currentY = transform.eulerAngles.y;
        turnDifference = currentY - previousY;
        currentTilt = Mathf.MoveTowards(currentTilt, turnDifference, tiltSpeed * Time.deltaTime);
        TiltPivot();
        
        eAgent.destination = target.position;

    }

    private void TiltPivot()
    {
        tiltPivot.localEulerAngles = new Vector3(0, 0, currentTilt * -tiltMultiplier);
    }

    private void LateUpdate()
    {
        previousY = currentY;
    }
}