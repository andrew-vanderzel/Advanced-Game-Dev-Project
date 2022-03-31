using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnyKey : MonoBehaviour
{
    public ScreenFader fader;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fader.fadeDirection = ScreenFader.Directions.Out;
        }
    }
}
