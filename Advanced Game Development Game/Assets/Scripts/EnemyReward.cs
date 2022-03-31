using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReward : MonoBehaviour
{
    public GameObject reward;
    public int rewardCount;
    public GameObject objectCenter;
    public float delay;
    public float timer;
    private bool gaveReward;
    private EnemyStats eStats;
    
    private void Start()
    {
        eStats = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        if (eStats.Health <= 0)
            timer -= 1 * Time.deltaTime;
        
        if (rewardCount > 0 && timer < 0 && eStats.Health <=0)
        {
            timer = delay;
            rewardCount -= 1;
            Instantiate(reward, objectCenter.transform.position, Quaternion.identity);
        }
    }
}
