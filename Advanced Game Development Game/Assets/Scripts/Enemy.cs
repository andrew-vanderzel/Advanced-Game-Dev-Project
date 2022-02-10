using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum EnemyMode
    {
        Idle,
        Attack,
        Dead
    }

    [Header("General Enemy Values")] public EnemyMode mode;

    public Transform target;
    public ParticleSystem smoke;
    public GameObject battery;
    public GameObject optionalEye;
    public GameObject explosion;
    public int batteryColorIndex;
    public int eyeColorIndex = 1;
    
    [Header("Idle Behavior")] public float idleDistance;

    private Color _currentBatteryColor;

    private bool _dead;
    private Vector3 _originalPosition;
    protected Vector3 currentPosition;


    protected NavMeshAgent eAgent;
    protected Vector3 previousPosition;
    protected EnemyStats stats;
    protected EnemyVision visionScript;
    
    public void Start()
    {
        visionScript = GetComponent<EnemyVision>();
        _originalPosition = transform.position;
        eAgent = GetComponent<NavMeshAgent>();
        stats = GetComponent<EnemyStats>();
        _currentBatteryColor = battery.GetComponent<Renderer>().materials[3].GetColor("_Color");
    }

    public void Update()
    {
        StandardMovement();

        if (mode == EnemyMode.Idle)
        {
            IdleBehavior();

            if (visionScript.CanSeePlayer())
            {
                mode = EnemyMode.Attack;
            }
        }

        if (mode == EnemyMode.Attack)
        {
            AttackBehavior();
        }

        if (mode == EnemyMode.Dead)
        {
            BaseDeath();
            SpecificDeath();
        }

        if (stats.health <= 0)
        {
            mode = EnemyMode.Dead;;
        }
    }

    public bool IsAttackMode()
    {
        return mode == EnemyMode.Attack;
    }

    protected bool IsIdleMode()
    {
        return mode == EnemyMode.Idle;
    }

    protected virtual void AttackBehavior()
    {
        if (eAgent)
            eAgent.destination = target.position;
    }

    private void IdleBehavior()
    {
        if (eAgent.remainingDistance - eAgent.stoppingDistance < 1)
        {
            Vector3 idleTarget = _originalPosition + Random.insideUnitSphere * idleDistance;

            if (!eAgent.hasPath)
                idleTarget = _originalPosition + Random.insideUnitSphere * idleDistance;

            eAgent.destination = idleTarget;
            Debug.DrawLine(_originalPosition, eAgent.destination, Color.red);
        }
        else
        {
            Debug.DrawLine(transform.position, eAgent.destination, Color.green);
        }
    }

    protected virtual void StandardMovement()
    {
        print("Not implemented!");
    }

    protected virtual void SpecificDeath()
    {
        print("Not implemented!");     
    }
    
    private void BaseDeath()
    {
        if (optionalEye)
            optionalEye.GetComponent<Renderer>().materials[eyeColorIndex]
                .SetColor("_Color", _currentBatteryColor);

        battery.GetComponent<Renderer>().materials[batteryColorIndex]
            .SetColor("_Color", _currentBatteryColor);
        _currentBatteryColor = Color.Lerp(_currentBatteryColor, Color.black, 2 * Time.deltaTime);
        explosion.SetActive(true);

        if (!_dead)
        {
            smoke.Play();
            _dead = true;
        }
    }
}