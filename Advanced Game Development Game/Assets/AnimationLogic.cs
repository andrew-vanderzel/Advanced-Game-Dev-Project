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
    public float turnRotation;
    
    private Vector3 smoothedDirection;
    private Animator anim;
    private float defaultY;
    private bool onGround; 
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        defaultY = transform.localPosition.y;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            smoothAim += aimSpeed * Time.deltaTime;
        else
            smoothAim -= aimSpeed * Time.deltaTime;

        if (GroundCheck())
            smoothAir -= 3 * Time.deltaTime;
        else
            smoothAir += 3 * Time.deltaTime;

        if (!GroundCheck() && onGround)
        {
            onGround = false;
            anim.SetTrigger("Jump");
        }

        anim.SetBool("onGround", GroundCheck());
        
        smoothAim = Mathf.Clamp(smoothAim, 0, 1);
        smoothAir = Mathf.Clamp(smoothAir, 0, 1) - (!Input.GetMouseButton(0)? 0 : 1);
        Vector3 target = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        smoothedDirection =
            Vector3.MoveTowards(smoothedDirection, target, smoothedAnimation * Time.deltaTime);
        
        anim.SetFloat("Horizontal", smoothedDirection.x);
        anim.SetFloat("Vertical", smoothedDirection.z);
        anim.SetLayerWeight(1, smoothAim);
        anim.SetLayerWeight(2, smoothAir);

        if (target != Vector3.zero || Input.GetMouseButton(0))
        {
            Vector3 targetRotation = Vector3.up * Camera.main.transform.eulerAngles.y;
            transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
            
        }
        transform.localPosition = new Vector3(0, defaultY, 0);
    }
    
    private bool GroundCheck()
    {
        if (Physics.Raycast(transform.parent.position, Vector3.down, out RaycastHit hit, 0.2f))
        {
            return true;
        }
        
        return false;
    }
    
}
