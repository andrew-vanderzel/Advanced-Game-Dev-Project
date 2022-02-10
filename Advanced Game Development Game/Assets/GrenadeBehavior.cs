using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBehavior : MonoBehaviour
{
    public float explosionTime;
    public bool startCountdown;
    public GameObject explosionPrefab;
    
    public void StartCountdown()
    {
        startCountdown = true;
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
                    e.health -= 3;
                }
                
                Destroy(gameObject);
            }
        }
    }
}
