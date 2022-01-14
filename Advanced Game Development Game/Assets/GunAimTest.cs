using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAimTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.up, out hit, 2000))
        {
            Debug.DrawLine(transform.position, hit.point);
        }
    }
}
