using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public GameObject grenade;
    public Transform grenadeSource;
    public float throwStrength;

    private Rigidbody rb;

    public void Throw()
    {
        FindObjectOfType<PlayerStats>().grenades -= 1;
        GameObject gInst = Instantiate(grenade, grenadeSource.position, Quaternion.identity);
        rb = gInst.GetComponent<Rigidbody>();
        
        float x = Screen.width / 2f;
        float y = Screen.height / 2f;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));
        Vector3 shootDir = ray.direction.normalized;
        
        rb.AddForce(shootDir * throwStrength, ForceMode.Impulse); 
    }
}
