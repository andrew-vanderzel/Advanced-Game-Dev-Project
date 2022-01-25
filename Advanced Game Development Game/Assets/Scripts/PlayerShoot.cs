using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletMark;
    public GameObject batteryMark;
    public Transform gun;
    public Animator[] muzzleFlashes;
    private Transform mainCam;
    private PlayerStats stats; 

    private void Start()
    {
        mainCam = Camera.main.transform;
        stats = transform.root.GetComponent<PlayerStats>();
        gun.GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        if (stats.IsDead())
        {
            gun.transform.parent = null;
            gun.GetComponent<Rigidbody>().isKinematic = false;
            gun.GetComponent<Collider>().enabled = true;
        }
    }

    public void Shoot()
    {
        for (int m = 0; m < muzzleFlashes.Length; m++)
        {
            muzzleFlashes[m].SetTrigger("Flash");
        }
        RaycastHit hit;
        
        if (Physics.Raycast(mainCam.position, mainCam.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider)
            {
                 
                var mark = Instantiate(bulletMark, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                mark.transform.parent = hit.collider.transform;
                
                if (hit.collider.CompareTag("PhysicsProp"))
                {
                    Vector3 dir = -(hit.point - hit.collider.transform.position).normalized;
                    Rigidbody objectRigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();
                    objectRigidbody.AddForce(dir * 5, ForceMode.Impulse);
                }

                if (hit.collider.CompareTag("Battery"))
                {
                    EnemyStats eStats = hit.collider.transform.root.GetComponent<EnemyStats>();
                    eStats.health -= 1;

                    if (eStats.health > 0)
                    {
                        print("Create mark?");
                        mark = Instantiate(batteryMark, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                        mark.transform.parent = hit.collider.transform;
                    }
                }
            }
        }
        
        
    }

}
