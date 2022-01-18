using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletMark;
    public Transform gun;
    private Transform mainCam;
    

    private void Start()
    {
        mainCam = Camera.main.transform;
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.position, mainCam.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider)
            {
                Instantiate(bulletMark, hit.point, Quaternion.identity);
            }
        }
        
        
    }

}
