using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetVelocity(Vector3 vel)
    {
        rb.velocity = vel * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        Invoke("DestroyBullet", 1);
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
