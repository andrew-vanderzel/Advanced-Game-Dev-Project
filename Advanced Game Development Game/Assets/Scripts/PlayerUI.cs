using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image playerHealthBar;
    public Image chargeBar;
    
    public PlayerStats stats;
    public PlayerJetpack jetpackScript;
    
    private void Update()
    {
        playerHealthBar.fillAmount = Mathf.InverseLerp(0, 100, stats.Health);
        chargeBar.fillAmount = Mathf.InverseLerp(0, 100, jetpackScript.ChargeAmount);
    }
}
