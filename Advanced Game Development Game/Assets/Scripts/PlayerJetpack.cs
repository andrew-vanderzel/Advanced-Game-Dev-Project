using UnityEngine;

public class PlayerJetpack : MonoBehaviour
{
    public float ChargeAmount { get; private set; }
    public float maxCharge = 100;
    [SerializeField] private float consumptionSpeed;
    [SerializeField] private float rechargeSpeed;
    [SerializeField] private float strength;
    
    private float _chargeDelay;
    private bool _canJetpack;
    private Rigidbody _rb;
    private PlayerMovement _pMovement;
    private JetpackParticles _jetpackParticles;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _pMovement = GetComponent<PlayerMovement>();
        _jetpackParticles = FindObjectOfType<JetpackParticles>();
        ChargeAmount = maxCharge;
    }

    private void Update()
    {
        JetpackFuelLogic();
        MovePlayer();

        bool canUseGravity = !Input.GetKey(KeyCode.LeftShift) || !_canJetpack;
        _rb.useGravity = canUseGravity;
    }
    
    private void MovePlayer()
    {
        if(!Input.GetKey(KeyCode.LeftShift) || !_canJetpack)
           _jetpackParticles.StopParticles();
        
        if (!Input.GetKey(KeyCode.LeftShift)) return;
        
        ChargeAmount -= 40 * Time.deltaTime;

        if (_canJetpack)
        {
            _jetpackParticles.StartParticles();
            Vector3 targetVelocity = new Vector3
            {
                x = _pMovement.MoveInput().x,
                y = _rb.velocity.y,
                z = _pMovement.MoveInput().z
            };
            targetVelocity.y += 0.6f * Time.deltaTime;
            targetVelocity.y = Mathf.Clamp(targetVelocity.y, 0, strength);
            _rb.velocity = targetVelocity;
            
            _pMovement.SetMovementOverride(targetVelocity, 0.2f);
        }
    }

    private void JetpackFuelLogic()
    {
        
        ChargeAmount = Mathf.Clamp(ChargeAmount, 0, maxCharge);
        _chargeDelay += 1 * Time.deltaTime;
        _canJetpack = ChargeAmount > 2;
        
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _chargeDelay = 0;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            ChargeAmount -= consumptionSpeed * Time.deltaTime;
        }
        else
        {
            if (_chargeDelay > 1)
            {
                ChargeAmount += rechargeSpeed * Time.deltaTime;
            }
        }
    }

}
