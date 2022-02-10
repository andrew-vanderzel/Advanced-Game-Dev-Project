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
                float distance = Vector3.Distance(e.transform.position, transform.position);

                if (distance < 2)
                {
                    e.health -= 10;
                }
                
                Destroy(gameObject);
            }
        }
        
        float closest = 20;
        GameObject targetEnemy = null;
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);

            if (dist < closest)
            {
                closest = dist;
                targetEnemy = enemy.gameObject;
            }
        }

        if (!targetEnemy) return;

        Vector3 dir = (targetEnemy.transform.position - transform.position).normalized;
        rb.AddForce(dir * seekStrength, ForceMode.Acceleration);    
         
    }
}
