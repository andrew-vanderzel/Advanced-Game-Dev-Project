using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public bool first;
    public float speed;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float firstDistance = Vector3.Distance(transform.position, startPosition);
        float secondDistance = Vector3.Distance(transform.position, endPosition);

        if (firstDistance < 0.2f && first)
            first = false;
        if (secondDistance < 0.2f && !first)
            first = true;
        
    }

    private void FixedUpdate()
    {

        Vector3 target = first ? startPosition : endPosition;
        Vector3 dir = (target - transform.position).normalized;
        rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
    }
}
