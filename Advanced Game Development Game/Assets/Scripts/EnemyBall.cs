
using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBall : Enemy
{
    public Transform ball;
    public Transform head;
    public Vector3 currRotateDirection;
    public Vector3 currentHeadDirection;
    public float increaseFactor;
    public float movementSpeed;
    public float turnSpeed;
    private NavMeshAgent eAgent;

    private Vector3 previousPos;
    private Vector3 currentPos;

    private new void Start()
    {
        base.Start();
        eAgent = GetComponent<NavMeshAgent>();
        currentPos = transform.position;
        previousPos = currentPos;
    }
    
    private new void Update()
    {
        base.Update();
        
        BallRotation(target.position);
    }

    private void BallRotation(Vector3 endPoint)
    {
        currentPos = transform.position;
        Vector3 ballDir = (currentPos - previousPos).normalized;
        Vector3 headDir = (endPoint - transform.position).normalized;
        
        Vector3 cross = Vector3.Cross(ballDir, Vector3.up);
        Vector3 headCross = Vector3.Cross(headDir, Vector3.up);
        
        currentHeadDirection = Vector3.MoveTowards(currentHeadDirection,headCross, turnSpeed * 2 * Time.deltaTime);
        Vector3 headForward = Vector3.Cross(currentHeadDirection, Vector3.up);
        headForward.y = 0;
        
        if(stats.health > 0)
            head.rotation = Quaternion.LookRotation(-headForward, Vector3.up);
        
        head.eulerAngles = new Vector3(-90, head.eulerAngles.y, head.eulerAngles.z);
        print(eAgent.velocity.magnitude * eAgent.speed * turnSpeed);
        ball.RotateAround(ball.position, -cross, eAgent.velocity.magnitude * eAgent.speed * turnSpeed * Time.deltaTime);

        if (stats.health > 0)
            eAgent.destination = endPoint;
        else
        {
            eAgent.speed -= 0.6f * Time.deltaTime;
        }
        
    }

    private void LateUpdate()
    {
        previousPos = transform.position;
    }
}
