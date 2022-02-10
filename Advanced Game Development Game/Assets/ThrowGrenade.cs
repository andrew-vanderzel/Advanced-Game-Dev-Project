using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public GameObject grenade;
    public Transform grenadeSource;
    public GameObject head;
    public float throwStrength;

    public void Throw()
    {
        print("Throwing");
        GameObject gInst = Instantiate(grenade, grenadeSource.position, Quaternion.identity);
        Rigidbody rb = gInst.GetComponent<Rigidbody>();
        rb.AddForce(Camera.main.transform.forward * throwStrength, ForceMode.Impulse); 
    }
}
