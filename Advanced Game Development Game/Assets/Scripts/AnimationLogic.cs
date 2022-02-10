using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class AnimationLogic : MonoBehaviour
{
    [SerializeField] private float smoothedAnimation;
    [SerializeField] private float smoothUpperBody = 6;
    [SerializeField] private float upperSpeed = 3;
    [SerializeField] private float groundDistance;
    
    private bool canJump;
    private Vector3 smoothedDirection;
    private Animator anim;
    private float defaultY;
    private PlayerStats pStats;
    private Transform cam;
    private bool isThrowing;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        defaultY = transform.localPosition.y;
        pStats = transform.root.GetComponent<PlayerStats>();
        cam = Camera.main.transform;
    }

    private void Update()
    {
        if (pStats.IsDead())
        {
            anim.SetBool("dead", true);
            smoothUpperBody -= upperSpeed * Time.deltaTime;
            anim.SetLayerWeight(1, smoothUpperBody);
            
            return;
        }

        UpperBodyAnimation();

        Vector3 target = WalkDirectionAnimation();
        if (target != Vector3.zero || Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            transform.eulerAngles = new Vector3(0, cam.eulerAngles.y, 0);
        }
        transform.localPosition = new Vector3(0, defaultY, 0);

        JumpIfGrounded();
        
        anim.SetBool("onGround", GroundCheck());
        
    }

    private void UpperBodyAnimation()
    {
        if (Input.GetMouseButton(0) || isThrowing)
            smoothUpperBody += upperSpeed * Time.deltaTime;
        else
            smoothUpperBody -= upperSpeed * Time.deltaTime;

        smoothUpperBody = Mathf.Clamp(smoothUpperBody, 0, 1);


        anim.SetLayerWeight(1, smoothUpperBody);
        
        anim.SetBool("Shoot", Input.GetMouseButton(0));
        if (Input.GetMouseButtonDown(1))
        {
            isThrowing = true;
            anim.ResetTrigger("Throw");
            anim.SetTrigger("Throw");
            
        }
    }

    public void SetThrowing()
    {
        isThrowing = true;
    }

    public void StopThrowing()
    {
        isThrowing = false;
        anim.ResetTrigger("Throw");
    }

    private Vector3 WalkDirectionAnimation()
    {
        Vector3 target = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        smoothedDirection =
            Vector3.MoveTowards(smoothedDirection, target, smoothedAnimation * Time.deltaTime);
        anim.SetFloat("Horizontal", smoothedDirection.x);
        anim.SetFloat("Vertical", smoothedDirection.z);

        return target;
    }

    private void JumpIfGrounded()
    {
        if (!GroundCheck() && canJump)
        {
            canJump = false;
            anim.SetTrigger("Jump");
        }

        if (GroundCheck())
        {
            canJump = true;
            anim.ResetTrigger("Jump");
        }
    }

    private bool GroundCheck()
    {
        if (Physics.Raycast(transform.parent.position + Vector3.up, Vector3.down, out RaycastHit hit, groundDistance))
        {
            return true;
        }
        
        return false;
    }
    
}
