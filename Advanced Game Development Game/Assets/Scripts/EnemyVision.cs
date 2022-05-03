using System;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float viewAngle;
    public float viewDistance;
    public bool inView;
    public Transform target;

    private void Start()
    {
        target = GetComponent<Enemy>().target;
    }

    private void Update()
    {
        Vector3 targetOffset = target.position;
        targetOffset.y += 0.5f;
        
        Vector3 dir = (transform.position - targetOffset).normalized;
        float angle = Vector3.Angle(dir, -transform.forward);
        float distance = Vector3.Distance(transform.position, targetOffset);
        inView = angle <= viewAngle && distance < viewDistance;
    }

    public bool CanSeePlayer()
    {
        return inView;
    }
}
