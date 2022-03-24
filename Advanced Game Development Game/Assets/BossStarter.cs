using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStarter : MonoBehaviour
{
    public DoorBehavior doorOne;
    public DoorBehavior doorTwo;
    public BossBehavior behaviorScript;
    public GameObject healthCanvas;
    
    public void StartBoss()
    {
        doorOne.open = false;
        doorTwo.open = false;
        behaviorScript.enabled = true;
        healthCanvas.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        StartBoss();
    }
}
