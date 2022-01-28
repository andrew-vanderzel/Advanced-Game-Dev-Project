using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamColorFlasher : MonoBehaviour
{
    public float flashSpeed;
    public float timer;
    public Gradient colors;
    
    private LineRenderer lr;
    
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        timer += flashSpeed * Time.deltaTime;

        if (timer > 1)
            timer = 0;

        lr.endColor = colors.Evaluate(timer);
        lr.startColor = colors.Evaluate(timer);
    }
}
