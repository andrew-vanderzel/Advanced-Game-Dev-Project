using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocketLauncher : MonoBehaviour
{
    public Transform[] rocketSources;
    public GameObject rocketPrefab;
    public float launchInterval;
    private float m_LaunchTimer;
    private int launchNum;
    private void Start()
    {
        m_LaunchTimer = launchInterval;
        launchNum = 0;
    }

    private void Update()
    {
        if (GetComponent<EnemyStats>().health <= 0)
            return;
        
        m_LaunchTimer -= 1 * Time.deltaTime;
        if (m_LaunchTimer <= 0)
        {
            
            LaunchRocket();
            Invoke(nameof(LaunchRocket), 0.8f);
            m_LaunchTimer = launchInterval;
        }
    }

    private void LaunchRocket()
    {
            rocketSources[launchNum].GetComponent<ParticleSystem>().Play();
            GameObject rocketInst = Instantiate(rocketPrefab, rocketSources[launchNum].position, Quaternion.identity);
            rocketInst.transform.forward = rocketSources[launchNum].forward;
            launchNum = launchNum == 0 ? 1 : 0;
    }
}
