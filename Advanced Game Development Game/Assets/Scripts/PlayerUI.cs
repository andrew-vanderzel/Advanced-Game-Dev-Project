using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image playerHealthBar;
    public Image chargeBar;
    public Text scrapText;
    public Text grenadeText;
    
    public Outline[] slotOutlines;
    
    public PlayerStats stats;
    public PlayerJetpack jetpackScript;

    private BulletChooser _bulletChooser;
    private GameStats gStats;
    private void Start()
    {
        _bulletChooser = FindObjectOfType<BulletChooser>();
        gStats = FindObjectOfType<GameStats>();
    }

    private void Update()
    {
        playerHealthBar.fillAmount = Mathf.InverseLerp(0, 100, stats.Health);
        //chargeBar.fillAmount = Mathf.InverseLerp(0, 100, jetpackScript.ChargeAmount);

        for(int i = 0; i < 3; i++)
        {
            slotOutlines[i].effectDistance =
                Vector2.one * (_bulletChooser.targetBulletIndex == i ? 10 : 5);
            
            slotOutlines[i].gameObject.SetActive(_bulletChooser.bulletUnlocks[i] == 1);
        }

        scrapText.text = "Scrap: " + gStats.scrapAmount.ToString("00");
        grenadeText.text = "Grenades: " + stats.grenades.ToString("00");
    }
}
