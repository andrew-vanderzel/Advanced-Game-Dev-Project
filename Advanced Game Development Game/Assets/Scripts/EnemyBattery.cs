using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattery : MonoBehaviour
{
    private EnemyStats stats;

    private void Start()
    {
        stats = transform.root.GetComponent<EnemyStats>();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            stats.health -= 1;
            print("hmm");
        }
    }
}
