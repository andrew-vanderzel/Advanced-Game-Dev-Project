using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawDamage : MonoBehaviour
{
    public float damage;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player"))
        {
            if(transform.root.GetComponent<EnemyStats>().health > 0)
                other.GetComponent<PlayerStats>().health -= damage * Time.deltaTime;
        }
    }
}
