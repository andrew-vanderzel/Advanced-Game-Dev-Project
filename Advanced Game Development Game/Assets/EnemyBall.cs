
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
        currentPos = transform.position;
        Vector3 ballDir = (currentPos - previousPos).normalized;
        Vector3 headDir = (target.position - transform.position).normalized;
        
        Vector3 cross = Vector3.Cross(ballDir, Vector3.up);
        Vector3 headCross = Vector3.Cross(headDir, Vector3.up);
        currRotateDirection = Vector3.MoveTowards(currRotateDirection, cross, turnSpeed * Time.deltaTime);
        currentHeadDirection = Vector3.MoveTowards(currentHeadDirection,headCross, turnSpeed * 2 * Time.deltaTime);
        
        Vector3 rotateForward = Vector3.Cross(currRotateDirection, Vector3.up);
        Vector3 headForward = Vector3.Cross(currentHeadDirection, Vector3.up);
        
        rotateForward.y = 0;
        headForward.y = 0;
            
        head.rotation = Quaternion.LookRotation(-headForward, Vector3.up);
        
        head.eulerAngles = new Vector3(-90, head.eulerAngles.y, head.eulerAngles.z);
        ball.RotateAround(ball.position, -cross, increaseFactor * Time.deltaTime);

        eAgent.destination = target.position;
    }

    private void LateUpdate()
    {
        previousPos = transform.position;
    }
}
