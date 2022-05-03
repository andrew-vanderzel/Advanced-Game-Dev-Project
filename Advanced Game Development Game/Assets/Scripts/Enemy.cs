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
    public GameObject exclamationObject;
    public float exclamationOffset;
     
    [Header("Idle Behavior")] public float idleDistance;

    private Color _currentBatteryColor;

    private bool _dead;
    private Vector3 _originalPosition;
    protected Vector3 currentPosition;
    private bool _seenPlayer;

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
        _seenPlayer = false;
    }

    public void Update()
    {
        StandardMovement();
        if (mode == EnemyMode.Dead)
        {
            BaseDeath();
            SpecificDeath();
        }

        if (stats.Health <= 0)
        {
            mode = EnemyMode.Dead;
            return;
        }

        if (mode == EnemyMode.Idle)
        {
            IdleBehavior();

            if (visionScript.CanSeePlayer())
            {
                
                mode = EnemyMode.Attack;
            }
        }

        if (mode == EnemyMode.Attack && stats.Health > 0)
        {
            
            if (!_seenPlayer)
            {
                var ex = Instantiate(exclamationObject, transform.position, Quaternion.identity);
                ex.GetComponent<ExclamationIcon>().enemy = gameObject;
                ex.GetComponent<ExclamationIcon>().offset = exclamationOffset;
                _seenPlayer = true;
            }
            AttackBehavior();
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
        if (!eAgent)
            return;
        
        if (eAgent.remainingDistance < 0.7f)
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
    }

    protected virtual void SpecificDeath()
    {
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
            AudioPlayer.ap.PlayExplosionSound();
            smoke.Play();
            _dead = true;
        }
    }
}