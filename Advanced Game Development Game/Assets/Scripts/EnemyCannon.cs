using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : Enemy
{
    [Header("Turret Variables")]
    public Transform swivelBase;
    public Transform tiltCannon;
    public GameObject sparkObject;
    public float turnSpeed;
    public Vector2 scaleRange;
    public float multiplier;
    public LineRenderer line;
    public Transform lineSource;
    public float viewAngle;
    public bool inView;
    
    private new void Start()
    {
        base.Start();
    }
    
    private new void Update()
    {
        base.Update();
        
        bool lineOfSight = false;
        Vector3 dir = (target.position + Vector3.up - lineSource.position).normalized;
        Debug.DrawRay(lineSource.position, dir * 100);
        if (Physics.Raycast(lineSource.position, dir, out var hit, 100))
        {
            if (hit.collider.CompareTag("Player"))
                lineOfSight = true;
        }
        
        if (stats.health > 0)
        {
            if (PlayerWithinAngle() && lineOfSight)
            {
                line.enabled = true;
                FaceTarget(target.position);
                FireLaser();
            }
            else
            {
                line.enabled = false;
            }
        }
        else
        {
            line.enabled = false;
        }
    }

    private bool PlayerWithinAngle()
    {
        Vector3 dir = (target.position - lineSource.position).normalized;
        float angle = Vector3.Angle(dir, lineSource.forward);
        return angle < viewAngle;
    }

    private void FireLaser()
    {
        line.SetPosition(0, lineSource.position);
        RaycastHit? hitData = LaserInfo();

        if (hitData.HasValue)
        {
            line.SetPosition(1, hitData.Value.point);
            if (hitData.Value.collider.CompareTag("Player"))
            {
                target.GetComponent<PlayerStats>().health -= 5 * Time.deltaTime; 
            }
        }
        else
        {
            line.SetPosition(1, lineSource.forward * 1000);
        }
    }

    private RaycastHit? LaserInfo()
    {
        if (Physics.Raycast(lineSource.position, lineSource.forward, out var hit, 1000))
        {
            return hit;
        }

        return null;

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

    private void SetSpark(Vector3 point)
    {
        sparkObject.SetActive(true);
        sparkObject.transform.position = point;
        sparkObject.GetComponent<ParticleSystem>().Play();
    }

    private void HideSpark()
    {
        sparkObject.SetActive(false);
        sparkObject.GetComponent<ParticleSystem>().Stop();
    }
}