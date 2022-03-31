using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int grenades;
    public int batteries;
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
        print("huh?");
        AudioPlayer.ap.PlayDamageSound();
        Health += val;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            ChangeHealth(-1);
        }
    }
}
