using System;
using UnityEngine;

public class SawDamage : MonoBehaviour
{
    public float damage;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player"))
        {
            if (transform.root.GetComponent<EnemyStats>().Health > 0)
            {
                other.GetComponent<PlayerStats>().ChangeHealth(-damage * Time.deltaTime, false); 
                
                if(source)
                    if (!source.isPlaying)
                    {
                        source.Play();
                    }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(source)
            source.Stop();
    }
}
