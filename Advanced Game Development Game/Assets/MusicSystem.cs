using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSystem : MonoBehaviour
{
    public List<AudioClip> songs;
    public AudioSource songSource;
    public Image fadeImage;
    public DoorOpener doorScript;
    private bool doorHadOpened;

    private void Start()
    {
        doorHadOpened = false;
    }

    public void NextSong()
    {
        songs.RemoveAt(0);
        songSource.clip = songs[0];
        songSource.Play();
    }

    private void Update()
    {
        if (doorScript.firstDoor.open)
        {
            doorHadOpened = true;
        }

        if (!doorHadOpened)
        {
            songSource.volume = Mathf.Clamp(1 - fadeImage.color.a, 0, 0.25f);
        }
        else
        {
            songSource.volume -= 1 * Time.deltaTime;
        }
    }
}
