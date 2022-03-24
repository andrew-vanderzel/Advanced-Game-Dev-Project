using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBehavior : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private float defaultScale;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        defaultScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = player.transform.position;
        offset.y += 1;
        
        transform.position = Vector3.MoveTowards(transform.position, offset,
            speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, offset);
        float targetScale = Mathf.InverseLerp(0, 3, distance) * defaultScale;
        transform.localScale = Vector3.one * targetScale;
        if (distance < 0.4f)
        {
            Destroy(gameObject);
            FindObjectOfType<GameStats>().scrapAmount += 5;
        }
    }
}
