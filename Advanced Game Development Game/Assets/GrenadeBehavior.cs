using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBehavior : MonoBehaviour
{
    public float explosionTime;
    public bool startCountdown;
    public GameObject explosionPrefab;
    public float detectStrength;
    public float seekStrength;

    private Rigidbody rb;
    
    
    public void StartCountdown()
    {
        startCountdown = true;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        explosionTime -= 1 * Time.deltaTime;

        if (explosionTime <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            foreach (var e in FindObjectsOfType<EnemyStats>())
            {
                if(e.transform.childCount == 0)
                    continue;
                float distance = Vector3.Distance(e.transform.GetChild(0).position, transform.position);

                if (distance < 1.6f)
                {
                    e.health -= 10;
                }
                
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rb.isKinematic = true;
            transform.parent = other.transform;
        }
    }
}
