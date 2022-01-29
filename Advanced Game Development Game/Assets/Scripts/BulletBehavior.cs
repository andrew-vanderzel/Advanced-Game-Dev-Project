using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed;
    public GameObject hitEffect;
    
    private Rigidbody rb;



    private void OnCollisionEnter(Collision other)
    {
        var hitObj = Instantiate(hitEffect, other.contacts[0].point, Quaternion.FromToRotation(Vector3.forward, other.contacts[0].normal));
        hitObj.transform.parent = other.collider.transform; 
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
