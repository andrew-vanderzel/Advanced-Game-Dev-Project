using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public int grenadeCost;
    public int batteryCost;
    public int[] jetpackUpgradeCosts;
    public int[] bulletUpgrade;
    public Text grenadeText;
    public Text batteryText;
    public Text scrapText;
    public Text bulletUpgradeText;
    public Text bulletUpgradeLabel;
    public Text jetpackUpgradeText;
    public Text jetpackUpgradeLabel;
    public Button bulletUpgradeButton;
    public Button jetpackUpgradeButton;
    private GameStats stats;
    private PlayerStats pStats;
    private int batteryUpgrade;

    private void Start()
    {
        batteryUpgrade = 1;
        stats = FindObjectOfType<GameStats>();
        pStats = FindObjectOfType<PlayerStats>();
    }

    private void Update()
    {
        grenadeText.text = pStats.grenades.ToString("00");
        scrapText.text = stats.scrapAmount.ToString("00");
        batteryText.text = "Batteries: " + pStats.batteries.ToString("00");
    }

    public void AddGrenade()
    {
        if (stats.scrapAmount >= grenadeCost)
        {
            stats.scrapAmount -= grenadeCost;
            pStats.grenades += 1;
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void AddBattery()
    {
        if (stats.scrapAmount >= batteryCost)
        {
            stats.scrapAmount -= batteryCost;
            pStats.batteries += 1;
        }
    }

    public void JetpackUpgrade()
    {
        if (stats.scrapAmount >= jetpackUpgradeCosts[batteryUpgrade])
        {
            stats.scrapAmount -= jetpackUpgradeCosts[batteryUpgrade];
            FindObjectOfType<PlayerJetpack>().maxCharge += 100;
            batteryUpgrade += 1;
            jetpackUpgradeLabel.text = "Jetpack Max Charge (" + batteryUpgrade + "/3)";
            stats.scrapAmount -= 10;
        }

        if (batteryUpgrade == 3)
        {
            jetpackUpgradeLabel.text = "All Unlocked";
            jetpackUpgradeText.text = "-";
            jetpackUpgradeButton.interactable = false;
        }

        batteryUpgrade = Mathf.Clamp(batteryUpgrade, 1, 2);
    }
    
    public void BulletUpgrade()
    {
        var bulletChooser = FindObjectOfType<BulletChooser>();
        
        if (bulletChooser.bulletUnlocks[1] == 0)
        {
            if (stats.scrapAmount >= bulletUpgrade[0])
            {
                FindObjectOfType<BulletChooser>().bulletUnlocks[1] = 1;
                bulletUpgradeText.text = bulletUpgrade[1].ToString();
                bulletUpgradeLabel.text = "Explosive Bullet";
                stats.scrapAmount -= bulletUpgrade[0];
            }
        }
        else if(bulletChooser.bulletUnlocks[2] == 0)
        {
            if (stats.scrapAmount >= bulletUpgrade[1])
            {
                FindObjectOfType<BulletChooser>().bulletUnlocks[2] = 1;
                bulletUpgradeText.text = "-";
                bulletUpgradeLabel.text = "All Unlocked";
                stats.scrapAmount -= bulletUpgrade[1];
                bulletUpgradeButton.interactable = false;
            }
        }
    }
        
    
}
