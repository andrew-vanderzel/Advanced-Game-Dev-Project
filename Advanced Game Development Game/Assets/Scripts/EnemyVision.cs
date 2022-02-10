using System;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float viewAngle;
    public float viewDistance;
    public bool inView;
    public Transform target;

    private void Update()
    {
        Vector3 dir = (transform.position - target.position).normalized;
        float angle = Vector3.Angle(dir, -transform.forward);
        print(angle);
        float distance = Vector3.Distance(transform.position, target.position);
        inView = angle <= viewAngle && distance < viewDistance;
    }

    public bool CanSeePlayer()
    {
        return inView;
    }
}
