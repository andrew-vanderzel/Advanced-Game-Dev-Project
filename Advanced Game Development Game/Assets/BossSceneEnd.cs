using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneEnd : MonoBehaviour
{
    private ScreenFader screenFade;

    private void Start()
    {
        screenFade = FindObjectOfType<ScreenFader>();
    }

    public void DoFadeOut()
    {
        screenFade.fadeDirection = ScreenFader.Directions.Out;
    }
}
