using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossBehavior : MonoBehaviour
{
    public Transform player;
    public EnemyStates state;

    public List<EnemyStates> attackOrder;
    
    private Rigidbody rb;
    private NavMeshAgent agent;
    private Animator anim;
    private EnemyStats eStats;

    public float timer;
    public Transform rocketSource;
    public GameObject rocket;
    public ParticleSystem rocketParticles;
    public ParticleSystem slamSmoke;

    public Animator sawAnimator;
    public Collider sawCollider;

    public bool sawOn;

    private Vector3 previousPlayerPosition;
    private Vector3 currentPlayerPosition;

    private bool didDeath;

    private void Start()
    {
        didDeath = false;
        rb = transform.parent.GetComponent<Rigidbody>();
        agent = transform.parent.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        eStats = transform.parent.GetComponent<EnemyStats>();
        timer = 4;
        sawOn = false;
        previousPlayerPosition = player.position;
        currentPlayerPosition = player.position;
        anim.SetTrigger("Start");
    }

    public enum EnemyStates
    {
        Standard,
        SlashCombo,
        JumpSlam,
        SwipeUp,
        SwipeDown,
        Spin,
        Shoot,
        Following,
        Jump,
        Idle,
    }

    private void Update()
    {
        currentPlayerPosition = player.position;
        

        if (eStats.Health <= 0)
        {
            if (didDeath == false)
            {
                didDeath = true;
                rb.velocity = Vector3.zero;
                transform.parent = null;
                anim.SetTrigger("Death");
            }
            return;
        }
        
        float distance = Vector3.Distance(transform.parent.position, player.position);
        print(distance);
        float speedMultiplier = timer <= 0? 1 : 0;
        print(speedMultiplier);
        agent.enabled = state == EnemyStates.Following && eStats.Health > 0;
        
        agent.speed = 1 + speedMultiplier * 5;
        if (state == EnemyStates.Following)
        {
            timer -= 1 * Time.deltaTime;

            agent.destination = player.position; 
            Vector3 dir = (player.position - transform.parent.position).normalized;
            float angle = Vector3.Angle(dir, transform.parent.forward);

            if (timer <= 0)
            {
                if (attackOrder[0] == EnemyStates.Shoot || attackOrder[0] == EnemyStates.JumpSlam)
                {
                    anim.SetTrigger("Attack");
                    timer = 3;
                    state = attackOrder[0];
                    attackOrder.RemoveAt(0);
                    attackOrder.Add(state);
                    SetAnimationValue();
                }
                else
                {
                    if (distance < 2)
                    {
                        anim.SetTrigger("Attack");
                        timer = 3;
                        state = attackOrder[0];
                        attackOrder.RemoveAt(0);
                        attackOrder.Add(state);
                        SetAnimationValue();
                    }
                    
                    if (distance > 18 && angle < 5)
                    {
                        anim.SetTrigger("Jump");
                        state = EnemyStates.Jump;
                    }
                }
            }
            else
            {
                if (distance > 18 && angle < 5)
                {
                    anim.SetTrigger("Jump");
                    state = EnemyStates.Jump;
                }
            }
        }
        
        anim.SetFloat("WalkSpeed", speedMultiplier);
        anim.SetBool("Idle", distance < agent.stoppingDistance);
        sawAnimator.speed = Mathf.MoveTowards(sawAnimator.speed, sawOn? 1f : 0, 10 * Time.deltaTime);
        sawCollider.enabled = sawOn;
    }

    private void LateUpdate()
    {
        previousPlayerPosition = currentPlayerPosition;
    }

    private void SetAnimationValue()
    {
        switch (state)
        {
            case(EnemyStates.Standard):
                anim.SetFloat("Blend", 0.25f);
                break;
            case(EnemyStates.SwipeUp):
                anim.SetFloat("Blend", 0.375f);
                break;
            case(EnemyStates.SwipeDown):
                anim.SetFloat("Blend", 0.5f);
                break;
            case(EnemyStates.SlashCombo):
                anim.SetFloat("Blend", 0.625f);
                break;
            case(EnemyStates.Spin):
                anim.SetFloat("Blend", 0.625f);
                break;
            case(EnemyStates.JumpSlam):
                anim.SetFloat("Blend", 0.875f);
                break;
            case(EnemyStates.Shoot):
                anim.SetFloat("Blend", 1);
                break;
        }
        
    }
    
    private void LaunchRocket()
    {
        rocketParticles.Play();
        GameObject rocketInst = Instantiate(rocket, rocketSource.position, Quaternion.identity);
        rocketInst.transform.forward = rocketSource.forward;
    }
    

    public void FixedAttackMovement(int val)
    {
        rb.velocity = transform.parent.forward * val;
    }
    
    public void AutoAttackMovement()
    {
        float distance = Vector3.Distance(transform.parent.position, player.position);

        rb.velocity = transform.parent.forward * distance * 0.65f;
    }
    
    public void StopAttackMovement()
    {
        rb.velocity = Vector3.zero;
        anim.ResetTrigger("Attack");
        anim.ResetTrigger("Jump");
        state = EnemyStates.Following;
    }

    public void TurnOnSaw()
    {
        sawOn = true;
    }
    
    public void TurnOffSaw()
    {
        sawOn = false;
    }

    public void PlaySmoke()
    {
        slamSmoke.Play();
    }

    public void SlamDamage()
    {
        FindObjectOfType<BossSlamDamage>().DoSlamDamage();
    }
    

    public void SetState(EnemyStates s)
    {
        state = s;
    }
}
