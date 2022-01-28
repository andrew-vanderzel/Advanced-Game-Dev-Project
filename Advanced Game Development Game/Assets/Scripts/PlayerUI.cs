using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image playerHealthBar;
    public PlayerStats stats;

    private void Update()
    {
        playerHealthBar.fillAmount = Mathf.InverseLerp(0, 100, stats.health);
    }
}
