using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;
    
    public bool IsDead()
    {
        return health <= 0;
    }
}
