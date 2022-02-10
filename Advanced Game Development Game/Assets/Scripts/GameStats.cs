using System;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats inst;
    
    public int scrapAmount;
    public PlayerStats pStats;
    
    
    private void Awake()
    {
        inst = this;
    }

    public bool IsGameOver()
    {
        return pStats.IsDead();
    }
    
    
}