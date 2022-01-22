using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pillar : Enemy
{
    public PillarStates state;
    public Transform pillarHead;
    public float rotateSpeed;
    public LineRenderer beam;
    public Transform beamStart;
    public Vector3 beamEnd;
    
    public enum PillarStates
    {
        Searching, Locked
    }

    private void Update()
    {
        if (state == PillarStates.Locked)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            dir.y = 0;
            var targetRotation = Quaternion.LookRotation(-dir);
            
            pillarHead.localRotation = Quaternion.Slerp(pillarHead.localRotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
        
        beam.SetPosition(0, beamStart.position);
    }

    private void FixedUpdate()
    {
        Vector3 end = beamStart.forward * 1000;

        if (Physics.Raycast(beamStart.position, beamStart.forward, out var hit, 1000))
        {
            if (hit.collider)
                end = hit.point;
        }
        
        beam.SetPosition(1, end);
        
    }
}
