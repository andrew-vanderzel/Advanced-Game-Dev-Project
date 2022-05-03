using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HeatSeekingBullet : MonoBehaviour
{
    public float turnSpeed;
    public float speed;
    public GameObject smallExplosion;
    private Transform target;
    private Rigidbody rb;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 targetOffset = target.position;
        targetOffset.y += 1;
        
        Vector3 playerDir = (targetOffset - transform.position).normalized;
        Quaternion targetRot = Quaternion.LookRotation(playerDir);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);

        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        ExplodeBullet();

        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
        if (other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<PlayerStats>().ChangeHealth(-10, true); 
        }
    }

    public void ExplodeBullet()
    {
        GameObject explosion = Instantiate(smallExplosion, transform.position, Quaternion.identity);
        Destroy(explosion, 1);
        Destroy(gameObject);
    }
}
