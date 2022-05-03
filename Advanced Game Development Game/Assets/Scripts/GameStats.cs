using System;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats inst;

    public int scrapAmount;
    public int enemiesRemaining;
    public PlayerStats pStats;
    private float originalEnemies;
    
    
    private void Awake()
    {
        inst = this;
    }

    public bool IsGameOver()
    {
        return pStats.IsDead();
    }

    private void Start()
    {
        originalEnemies = FindObjectsOfType<EnemyStats>().Length;
    }

    private void Update()
    {
        enemiesRemaining = FindObjectsOfType<EnemyStats>().Count(i => i.Health > 0);
        scrapAmount = Mathf.Clamp(scrapAmount, 0, 99);
    }
}