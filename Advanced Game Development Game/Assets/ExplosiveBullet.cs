using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class ExplosiveBullet : BulletBehavior
{
    public float explosionTime;
    public GameObject explosion;
    public bool attached;
    public float flashTimer;
    public Material flashMaterial;

    private void Start()
    {
        flashTimer = explosionTime / 3;
        flashMaterial = transform.GetChild(0).GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (attached)
        {
            explosionTime -= 1 * Time.deltaTime;
        }

        if (explosionTime <= 0)
        {
            float dist = 4;
            EnemyStats targetEnemy = null;

            foreach (var enemy in FindObjectsOfType<EnemyStats>())
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < dist)
                {
                    targetEnemy = enemy;
                    dist = distance;
                }

            }

            if (targetEnemy)
            {
                targetEnemy.health -= 3;
            }
             
            Instantiate(explosion, transform.position, Quaternion.identity);
            DestroyBullet();
        }

        flashTimer -= 1 * Time.deltaTime;

        if (flashTimer < 0)
        {
            flashMaterial.color = flashMaterial.color == Color.black? Color.red : Color.black;
            flashTimer = explosionTime / 3;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
            return;
        
        transform.parent = other.collider.transform;
        GetComponent<Rigidbody>().collisionDetectionMode =
            CollisionDetectionMode.ContinuousSpeculative;
        
        GetComponent<Rigidbody>().isKinematic = true;
        attached = true;
    }
}
