using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEditor.U2D;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed;
    public GameObject hitEffect;
    public GameObject batteryHitEffect; 
    private Rigidbody rb;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Battery"))
        {
            if (other.collider.transform.root.GetComponent<EnemyStats>().health > 0 && !other.collider.CompareTag("Player"))
            {
                CreateHitEffect(batteryHitEffect, other);
                transform.position = Vector3.up;
            }
        }
        else
        {
            CreateHitEffect(hitEffect, other);
        }

        Invoke(nameof(DestroyBullet), 1f);
    }

    private void CreateHitEffect(GameObject obj, Collision colInfo)
    {
        GameObject hitObj = Instantiate(obj, colInfo.contacts[0].point, Quaternion.FromToRotation(Vector3.forward, colInfo.contacts[0].normal));
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        hitObj.transform.parent = colInfo.collider.transform;
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
