using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public enum Directions
    {
        In,
        Out
    }

    public Directions fadeDirection;
    public Image fader;
    public float speed;
    public string targetScene;
    public AudioSource source;

    private void Start()
    {
        fader.color = Color.black;
    }

    private void Update()
    {
        if (fadeDirection == Directions.In)
        {
            Color c = fader.color;
            c.a -= speed * Time.deltaTime;
            c.a = Mathf.Clamp(c.a, 0, 1);
            fader.color = c;
        }
        else
        {
            Color c = fader.color;
            c.a += speed * Time.deltaTime;
            c.a = Mathf.Clamp(c.a, 0, 1);
            fader.color = c;
        }
        
        

        if (fadeDirection == Directions.Out && fader.color.a >= 1)
        {
            SceneManager.LoadScene(targetScene);
        }

        if (source)
            source.volume = 1 - fader.color.a;
    }
}
