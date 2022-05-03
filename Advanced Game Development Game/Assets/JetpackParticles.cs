using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackParticles : MonoBehaviour
{
    private ParticleSystem pSystem;
    private AudioSource source;

    private void Start()
    {
        pSystem = GetComponent<ParticleSystem>();
        source = GetComponent<AudioSource>();
    }

    public void StartParticles()
    {
        if(!pSystem.isPlaying)
            pSystem.Play();

        if (!source.isPlaying)
        {
            source.Play();
        }
    }

    public void StopParticles()
    {
        pSystem.Stop();
        source.Stop();
    }
}
