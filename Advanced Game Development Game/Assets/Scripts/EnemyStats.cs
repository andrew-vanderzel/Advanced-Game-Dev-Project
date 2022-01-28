using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health;
    
    public void DoDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, Mathf.Infinity);
    }
}
