using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float Health { get; private set; }
    public float JetpackCharge { get; set; }

    private void Start()
    {
        Health = 100;
        JetpackCharge = 100;
    }

    public bool IsDead()
    {
        return Health <= 0;
    }

    public void ChangeHealth(float val)
    {
        Health += val;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            Health -= 1;
        }
    }
}
