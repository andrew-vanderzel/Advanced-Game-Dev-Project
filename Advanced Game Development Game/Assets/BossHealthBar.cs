using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public EnemyStats bossStats;
    public Image healthBar;
    
    private void Update()
    {
        healthBar.fillAmount = bossStats.health / 100;
    }
}
