using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    public Vector3 openLocation;
    public float speed;
    public bool open;
    private Vector3 defaultLocation;

    private void Start()
    {
        defaultLocation = transform.localPosition;
    }

    private void Update()
    {
        if (open)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, openLocation,
                speed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, defaultLocation,
                speed * Time.deltaTime);
        }
    }
}
