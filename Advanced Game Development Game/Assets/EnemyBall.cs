
using System;
using UnityEngine;

public class EnemyBall : Enemy
{
    public Transform ball;
    public Vector3 currRotateDirection;
    public float increaseFactor;
    private new void Update()
    {
        base.Update();
        increaseFactor += 1 * Time.deltaTime;
        Vector3 dir = (target.position - transform.position).normalized;
        currRotateDirection = Vector3.MoveTowards(currRotateDirection, dir, 3 * Time.deltaTime);

        dir.y = 0;
        ball.transform.RotateAround(target.transform.GetChild(1).right, dir, 200 * Time.deltaTime);

    }
}
