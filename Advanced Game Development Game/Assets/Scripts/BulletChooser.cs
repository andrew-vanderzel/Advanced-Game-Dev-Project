using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletChooser : MonoBehaviour
{
    public GameObject[] bulletPrefabs;
    public GameObject currentBullet;
    public int targetBulletIndex;
    public int[] bulletUnlocks;
    
    private void Start()
    {
        targetBulletIndex = 0;
    }

    private void Update()
    {
        ChangeBullet();
        currentBullet = bulletPrefabs[targetBulletIndex];
    }

    private void ChangeBullet()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && bulletUnlocks[0] == 1)
            targetBulletIndex = 0;

        if (Input.GetKeyDown(KeyCode.Alpha2) && bulletUnlocks[1] == 1)
            targetBulletIndex = 1;

        if (Input.GetKeyDown(KeyCode.Alpha3) && bulletUnlocks[2] == 1)
            targetBulletIndex = 2;
    }
}
