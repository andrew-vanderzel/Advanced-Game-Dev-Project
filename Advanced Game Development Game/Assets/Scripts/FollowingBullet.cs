using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingBullet : BulletBehavior
{
    public GameObject marker;
    public float rotateSpeed;
    public GameObject markerInst;
    public LayerMask mask;

    private Vector3 endPoint;
    private Transform player;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        markerInst = Instantiate(marker, Vector3.zero, Quaternion.identity);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 10000, mask))
        {
            markerInst.transform.position = hit.point;
            markerInst.transform.parent = hit.collider.transform.root;
            
        }
        else
        {
            endPoint = transform.forward * 1000000;
            markerInst.transform.position = endPoint;
        }
    }

    void Update()
    {
        if(!marker)
            Destroy(gameObject);

        if (!markerInst)
            return;
        
        Vector3 dir = (markerInst.transform.position - transform.position).normalized;
        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);

        rb.velocity = dir * 120 * speed;
        float scale = Vector3.Distance(player.position, markerInst.transform.position);
        scale *= 0.1f;
        markerInst.transform.localScale = Vector3.one * scale;
    }

    protected override void ExtraDeathLogic()
    {
        Destroy(markerInst);
    }
}
