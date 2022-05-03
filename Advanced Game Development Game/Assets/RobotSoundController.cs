using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSoundController : MonoBehaviour
{
    private AudioSource _source;
    private EnemyStats _eStats;
    
    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _eStats = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        if (_eStats.Health <= 0)
        {
            _source.volume -= 0.3f * Time.deltaTime;
            _source.pitch -= 0.3f * Time.deltaTime;
        }
    }
}
