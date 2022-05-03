using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBotGun : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletSource;
    public float fireRate;
    public float timer;
    public bool inAngle;
    public Transform target;
    public float angleThreshold;

    private EnemyStats eStats;
    private Enemy _enemyScript;
    
    
    private void Start()
    {
        _enemyScript = transform.root.GetComponent<Enemy>();
        eStats = transform.root.GetComponent<EnemyStats>();
    }

    private void Update()
    {
        if (eStats.Health <= 0 || !_enemyScript.IsAttackMode())
            return;
        
        Vector3 targetOffset = target.position;
        targetOffset.y  += 1.5f;
        
        Vector3 playerDir = (targetOffset - transform.position).normalized;
        float angle = Vector3.Angle(playerDir, -transform.right);
        inAngle = angle < angleThreshold;


        if (inAngle)
        {
            if (timer <= 0)
            {
                timer = fireRate;
                GameObject bulletInst = Instantiate(bullet, bulletSource.position, Quaternion.identity);
                bulletInst.GetComponent<Rigidbody>().velocity = bulletSource.forward * 28;
                AudioPlayer.ap.PlayShootSound();
            }

            timer -= 1 * Time.deltaTime;
        }
        else
        {
            timer = fireRate;
        }
    }
}
