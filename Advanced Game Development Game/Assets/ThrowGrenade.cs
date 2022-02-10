using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public GameObject grenade;
    public Transform grenadeSource;
    public GameObject head;
    public float throwStrength;

    private Rigidbody rb;

    public void Throw()
    {
        print("Throwing");
        GameObject gInst = Instantiate(grenade, grenadeSource.position, Quaternion.identity);
        rb = gInst.GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * throwStrength, ForceMode.Impulse); 
    }
}
