using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform source;
    public Vector3 offset;

    private void Update()
    {
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(this.bullet, source.position, Quaternion.identity);
        var dir = (Camera.main.transform.forward * 100 - transform.position).normalized + offset;
        bullet.GetComponent<Rigidbody>().velocity = dir * 25;
    }
}
