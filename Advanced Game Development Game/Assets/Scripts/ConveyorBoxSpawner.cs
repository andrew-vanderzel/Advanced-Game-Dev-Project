using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBoxSpawner : MonoBehaviour
{
    public GameObject box;
    public float spawnRate;
    public float deleteRate;
    public float deleteTimer;
    public float timer;
    public Vector3 direction;
    public Queue<GameObject> boxes;

    private void Start()
    {
        deleteTimer = deleteRate;
        boxes = new Queue<GameObject>();
    }

    private void Update()
    {
        timer -= 1 * Time.deltaTime;
        deleteTimer -= 1 * Time.deltaTime;
        if (timer <= 0)
        {
            timer = spawnRate;
            GameObject inst = Instantiate(box, transform.position, Quaternion.identity);
            boxes.Enqueue(inst);
        }
        

        if (deleteTimer <= 0)
        {
            var boxInst = boxes.Dequeue();
            Destroy(boxInst);
            deleteTimer = deleteRate;
        }
        
        
    }

    private void FixedUpdate()
    {
        foreach (var b in boxes)
        {
            b.GetComponent<Rigidbody>().velocity = direction;
        }
    }
}
