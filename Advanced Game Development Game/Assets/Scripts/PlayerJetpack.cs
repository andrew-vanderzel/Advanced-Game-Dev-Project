using UnityEngine;

public class PlayerJetpack : MonoBehaviour
{
    public float ChargeAmount { get; private set; }
    [SerializeField] private float consumptionSpeed;
    [SerializeField] private float rechargeSpeed;
    [SerializeField] private float strength;
    
    private float _chargeDelay;
    private bool _canJetpack;
    private Rigidbody _rb;
    private PlayerMovement _pMovement;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _pMovement = GetComponent<PlayerMovement>();
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
        if (!Input.GetKey(KeyCode.LeftShift)) return;
        
        ChargeAmount -= 40 * Time.deltaTime;

        if (_canJetpack)
        {
            Vector3 targetVelocity = new Vector3
            {
                x = _pMovement.MoveInput().x,
                y = Mathf.MoveTowards(_rb.velocity.y, strength, 6 * Time.deltaTime),
                z = _pMovement.MoveInput().z
            };

            _rb.velocity = targetVelocity;
            _pMovement.SetMovementOverride(targetVelocity, 0.2f);
        }
    }

    private void JetpackFuelLogic()
    {
        
        ChargeAmount = Mathf.Clamp(ChargeAmount, 0, 100);
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
