using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class PlayerFoostepSounds : MonoBehaviour
{
    public AudioClip leftFoot;
    public AudioClip rightFoot;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayLeftFoot()
    {
        source.PlayOneShot(leftFoot);
    }
    
    
    public void PlayRightFoot()
    {
        source.PlayOneShot(rightFoot);
    }
}
