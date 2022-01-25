using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public bool dead;
    public ParticleSystem smoke;
    public GameObject battery;
    public GameObject explosion;
    public int batteryColorIndex;
    protected EnemyStats stats;
    private Color currentBatteryColor;

    public void Start()
    {
        stats = GetComponent<EnemyStats>();
        currentBatteryColor = battery.GetComponent<Renderer>().materials[3].GetColor("_Color");

    }

    public void DamagePlayer(float damage)
    {
        target.GetComponent<PlayerStats>().health -= damage * Time.deltaTime;
    }

    public void Update()
    {
        Death();
    }

    private void Death()
    {
        if (stats.health <= 0)
        {
            battery.GetComponent<Renderer>().materials[3].SetColor("_Color", currentBatteryColor);
            currentBatteryColor = Color.Lerp(currentBatteryColor, Color.black, 2 * Time.deltaTime);
            explosion.SetActive(true);
            if (!dead)
            {
                smoke.Play();
                dead = true;
            }

        }
        
    }
}
