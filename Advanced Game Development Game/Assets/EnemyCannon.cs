using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : Enemy
{
    public Transform swivelBase;
    public Transform tiltCannon;
    public float turnSpeed;
    public Vector2 scaleRange;
    public float multiplier;
    public LineRenderer line;
    public Transform lineSource;

    private void Start()
    {
    }

    private void Update()
    {
        FaceTarget(target.position);
        FireLaser();
    }

    private void FireLaser()
    {
        line.SetPosition(0, lineSource.position);
        Vector3 end = lineSource.forward * 1000;

        if (Physics.Raycast(lineSource.position, lineSource.forward, out var hit, 1000))
        {
            if (hit.collider)
                end = hit.point;
        }
        
        line.SetPosition(1, end);
    }
    
    private void FaceTarget(Vector3 targetPosition)
    {
        Vector3 playerDirection = (targetPosition - transform.position).normalized;
        Vector3 swivelDir = new Vector3(playerDirection.x , 0, playerDirection.z);
        Vector3 tiltDir = Vector3.right * playerDirection.y * multiplier;
        Quaternion targetRot = Quaternion.Euler(tiltDir);

        tiltCannon.localRotation =
            Quaternion.Lerp(tiltCannon.localRotation, targetRot, turnSpeed * Time.deltaTime);
        
        Quaternion swivelRotTarget = Quaternion.LookRotation(-swivelDir, Vector3.up);
        swivelBase.rotation = Quaternion.Lerp(swivelBase.rotation, swivelRotTarget, turnSpeed * Time.deltaTime);
        
    }
}
