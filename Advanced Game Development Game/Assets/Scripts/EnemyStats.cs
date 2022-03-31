using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health;

    public float Health
    {
        get => health;
        set
        {
            AudioPlayer.ap.PlayDamageSound();
            health = value;
        }
            
    }

    public void DoDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, Mathf.Infinity);
    }

}
