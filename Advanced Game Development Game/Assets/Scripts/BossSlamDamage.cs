using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlamDamage : MonoBehaviour
{
    public float damage;
    public bool playerInRadius;

    public void DoSlamDamage()
    {
        if (playerInRadius)
        {
            FindObjectOfType<PlayerStats>().ChangeHealth(-damage, true);
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRadius = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRadius = false;
        }
    }
}
