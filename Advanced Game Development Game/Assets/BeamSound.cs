using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSound : MonoBehaviour
{
    public LineRenderer laserLine;
    private AudioSource source;
    
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (laserLine.enabled)
        {
            source.mute = false;
        }
        else
        {
            source.mute = true;
        }
    }
}
