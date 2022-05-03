using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationIcon : MonoBehaviour
{
    public float appearanceTime;
    public GameObject enemy;
    public float offset;

    private void Update()
    {
        Vector3 followPosition = enemy.transform.GetChild(0).position;
        followPosition.y += offset;
        transform.position = followPosition;
        transform.LookAt(Camera.main.transform.position);
        
        appearanceTime -= 1 * Time.deltaTime;
        if(appearanceTime <= 0)
            Destroy(gameObject);
        
        
    }
}
