using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossSounds : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    public AudioClip slamSound;
    public AudioClip sawSound;
    public AudioClip swingSound;
    public AudioClip jumpSound;
    public AudioSource sawSource;
    public bool sawOn;

    private void Start()
    {
        sawSource.pitch = 0.4f;
        sawSource.volume = 0;
    }

    private void Update()
    {

        if (sawOn)
        {
            sawSource.volume = Mathf.MoveTowards(sawSource.volume, 0.08f, 1f * Time.deltaTime);
            sawSource.pitch = Mathf.MoveTowards(sawSource.pitch, 1, 0.28f * Time.deltaTime);
        }
        else
        {
            sawSource.volume = Mathf.MoveTowards(sawSource.volume, 0, 0.1f * Time.deltaTime);
            sawSource.pitch = Mathf.MoveTowards(sawSource.pitch, 0.4f, 0.28f * Time.deltaTime);

            if (sawSource.volume == 0)
            {
                sawSource.Stop();
            }
        }
        
    }

    public void PlayRandomFootstep()
    {
        int i = Random.Range(0, 4);
        PlaySound(footstepSounds[i]);
    }

    public void PlaySlamSound()
    {
        PlaySound(slamSound);
    }

    public void PlaySwingSound()
    {
        PlaySound(swingSound);
    }

    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }

    public void StartSaw()
    {
        sawSource.Play();
        sawOn = true;
    }

    public void StopSaw()
    {
        sawOn = false;
    }

    private void PlaySound(AudioClip clip)
        => AudioSource.PlayClipAtPoint(clip, transform.position);

}
