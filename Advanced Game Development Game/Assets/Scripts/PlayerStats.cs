using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;


    private void Update()
    {
        health -= 1 * Time.deltaTime;
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}
