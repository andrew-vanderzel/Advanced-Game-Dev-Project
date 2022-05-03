using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public float explosiveDistance;
    public int barrelHealth;
    public GameObject explosion;
    public float explosionScale;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Explode()
    {
        source.enabled = true;
        var enemiesToDamage = FindObjectsOfType<EnemyStats>().Where(
            i => Vector3.Distance(i.transform.position, transform.position) < explosiveDistance);

        var barrelsToDestroy = FindObjectsOfType<ExplosiveBarrel>().Where(
            i => Vector3.Distance(i.transform.position, transform.position) < explosiveDistance
            && i.barrelHealth > 0);
        
        foreach (var e in enemiesToDamage)
            e.DoDamage(10);
        foreach (var e in barrelsToDestroy)
        {
            e.barrelHealth = 0;
            e.HealthCheck();
        }
    }
    
    [ContextMenu("Check")]
    public void HealthCheck()
    {
        if (barrelHealth <= 0)
        {
            GameObject inst = Instantiate(explosion, transform.position, Quaternion.identity);
            inst.transform.localScale = Vector3.one * explosionScale;
            Explode();
            Destroy(gameObject, 0.4f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            barrelHealth -= 1;
        }
        
        HealthCheck();
    }
}
