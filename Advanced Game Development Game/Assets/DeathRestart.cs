using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathRestart : MonoBehaviour
{
    public PlayerStats pStats;
    public Image fader;
    public Text deathText;
    public AudioSource musicAudio;
    
    private void Update()
    {
        if (pStats.Health <= 0)
        {
            musicAudio.pitch = Mathf.MoveTowards(musicAudio.pitch, 0.77f, 3 * Time.deltaTime);
            Color c = fader.color;
            c.a += 0.3f * Time.deltaTime;
            fader.color = c;

            if (c.a >= 1)
            {
                SceneManager.LoadScene("SampleScene");
            }
            
            c = deathText.color;
            c.a += 3 * Time.deltaTime;
            deathText.color = c;

        }
    }
}
