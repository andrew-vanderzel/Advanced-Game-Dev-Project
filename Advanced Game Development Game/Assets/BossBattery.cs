using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattery : MonoBehaviour
{
    public Transform followObject;
    public EnemyStats eStats;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = followObject.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            eStats.Health -= 1;
        }
    }
}
