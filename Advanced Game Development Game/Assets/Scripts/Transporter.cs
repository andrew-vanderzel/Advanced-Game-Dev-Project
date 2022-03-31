using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transporter : MonoBehaviour
{
    public CurrentObjective objective;
    public Image fadeImage;
    public GameObject player;
    public Vector3 teleportLocation;
    public Vector3 targetRotation;
    public bool fadeBack;
    public bool fadeIn;

    private void Start()
    {
        fadeIn = false;
        fadeImage.color = Color.black;
        GetComponent<MeshRenderer>().enabled = true;
        
    }

    private void Update()
    {
        if (fadeIn == false)
        {
            Color fadeColor = fadeImage.color;
            fadeColor.a -= 0.4f * Time.deltaTime;
            fadeImage.color = fadeColor;

            if (fadeColor.a <= 0)
            {
                fadeIn = true;
            }
        }
        
        if (objective.objectives.Count == 0 && !fadeBack)
        {
            GetComponent<MeshRenderer>().enabled = true;
            Color fadeColor = fadeImage.color;
            fadeColor.a += 0.5f * Time.deltaTime;
            fadeImage.color = fadeColor;

            if (fadeImage.color.a >= 1)
            {
                player.transform.position = teleportLocation;
                player.transform.eulerAngles = targetRotation;
                FindObjectOfType<MusicSystem>().NextSong();
                fadeBack = true;
            }

        }

        if (fadeBack)
        {
            objective.SecondLevelSet();
            Color fadeColor = fadeImage.color;
            fadeColor.a -= 0.5f * Time.deltaTime;
            fadeImage.color = fadeColor;
        }
    }
}
