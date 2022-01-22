using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class AnimationLogic : MonoBehaviour
{
    public float smoothedAnimation;
    public float smoothAim;
    public float smoothAir;
    public float aimSpeed;
    public float groundDistance;
    public bool onGround;
    private bool canJump;
    
    private Vector3 smoothedDirection;
    private Animator anim;
    private float defaultY;
    private PlayerStats pStats;
    
    
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        defaultY = transform.localPosition.y;
        pStats = transform.root.GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (pStats.IsDead())
        {
            anim.SetBool("dead", true);
            smoothAim -= aimSpeed * Time.deltaTime;
            anim.SetLayerWeight(1, smoothAim);
            
            return;
        }

        if (Input.GetMouseButton(0))
            smoothAim += aimSpeed * Time.deltaTime;
        else
            smoothAim -= aimSpeed * Time.deltaTime;

        if (GroundCheck())
            smoothAir -= 7 * Time.deltaTime;
        else
            smoothAir += 7 * Time.deltaTime;

        if (!GroundCheck() && canJump)
        {
            canJump = false;    
            print("jump");
            anim.SetTrigger("Jump");
        }

        if (GroundCheck())
        {
            canJump = true;
            anim.ResetTrigger("Jump");
        }
        
        onGround = GroundCheck();
        
        anim.SetBool("onGround", GroundCheck());
        
        smoothAim = Mathf.Clamp(smoothAim, 0, 1);
        smoothAir = Mathf.Clamp(smoothAir, 0, 1) - (!Input.GetMouseButton(0)? 0 : 1);
        Vector3 target = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        smoothedDirection =
            Vector3.MoveTowards(smoothedDirection, target, smoothedAnimation * Time.deltaTime);
        
        anim.SetFloat("Horizontal", smoothedDirection.x);
        anim.SetFloat("Vertical", smoothedDirection.z);
        anim.SetLayerWeight(1, smoothAim);

        if (target != Vector3.zero || Input.GetMouseButton(0))
        {
            Vector3 targetRotation = Vector3.up * Camera.main.transform.eulerAngles.y;
            transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
            
        }
        transform.localPosition = new Vector3(0, defaultY, 0);

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
