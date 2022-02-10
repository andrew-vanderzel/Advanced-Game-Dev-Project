using UnityEngine;
using UnityEngine.AI;

public class EnemyFlying : Enemy
{
   public float turnSpeed;
   public Transform batteryOne;
   public Transform batteryTwo;
   public Transform propeller;
   public Transform gunPivot;
   private Rigidbody _rb;
   
   private float _propSpeed;
   private float smoothMoveVal = 10;
   private float _previousY, _currentY, _smoothY;
   private float smoothYVal = 2;
   private float gunTiltSpeed = 5;
   private Vector3 _smoothMoveDir;
   private Vector3 moveDir;
   private Vector3 deathDirection;
   private GameObject previousChild; 
   
   public new void Start()
   {
      base.Start();
      _rb = transform.GetChild(0).GetChild(0).GetComponent<Rigidbody>();
      _propSpeed = 600;
      currentPosition = transform.position;
      previousPosition = transform.position;
      _previousY = transform.eulerAngles.y;
      _currentY = _previousY;
   }

   protected override void StandardMovement()
   {
      currentPosition = transform.position;
      _currentY = transform.eulerAngles.y;
      
      moveDir = (previousPosition - currentPosition);
      moveDir.y = 0;
      
      propeller.Rotate(new Vector3(0, _propSpeed * Time.deltaTime, 0), Space.Self);
      if (stats.health <= 0)
      {
         SpecificDeath();
         return;
      }
      
      TiltBot();
   }

   protected override void AttackBehavior()
   {
         AimGun();
         RotateBot(target.position);
         eAgent.destination = target.position;
   }

   private void TiltBot()
   {
      if (transform.childCount == 0)
         return;
      
      _smoothMoveDir = Vector3.Lerp(_smoothMoveDir, moveDir, smoothMoveVal * Time.deltaTime);
      float moveInfluence = _smoothMoveDir.magnitude * 500;
      float yChange = _currentY - _previousY;
      yChange = Mathf.Clamp(yChange, -2, 2); _smoothY = Mathf.Lerp(_smoothY, yChange * -120, smoothYVal * Time.deltaTime);
      Vector3 rotTarget = new Vector3(moveInfluence, 0, _smoothY);
      transform.GetChild(0).localEulerAngles = rotTarget;
   }

   private void RotateBot(Vector3 endPos)
   {
      Vector3 dir = (endPos - transform.position).normalized;
      dir.y = 0;
      Quaternion endPoint = Quaternion.LookRotation(dir);
      Quaternion currentDir = Quaternion.Slerp(transform.rotation, endPoint, turnSpeed * Time.deltaTime);
      transform.rotation = currentDir;
      
   }

   private void AimGun()
   {
      
      Vector3 targetOffset = target.position;
      targetOffset.y += 2;
      Vector3 playerDir = (targetOffset - gunPivot.position).normalized;
      Debug.DrawRay(gunPivot.position, playerDir * 3, Color.green); 
      Vector3 tiltDir = Vector3.up * playerDir.y * 120;
      Quaternion quatTarg = Quaternion.Euler(tiltDir);
      
      gunPivot.localRotation = Quaternion.Lerp(gunPivot.localRotation, quatTarg, gunTiltSpeed * Time.deltaTime);
   }
    
   protected override void SpecificDeath()
   {
      _propSpeed = Mathf.MoveTowards(_propSpeed, 0, 200 * Time.deltaTime);
      if (transform.childCount > 0)
      {
         deathDirection = moveDir.normalized * eAgent.velocity.magnitude;
         deathDirection.y = _rb.velocity.y;
         
         batteryOne.gameObject.SetActive(false);
         batteryTwo.gameObject.SetActive(false);
         previousChild = transform.GetChild(0).gameObject;
         transform.GetChild(0).parent = null;
         _rb.isKinematic = false;
         Vector3 forcePos = _rb.position + transform.up;
         Vector3 randomForce = _rb.gameObject.transform.up * Random.Range(10, 30) * (Random.value > 0.5f ? 1 : -1);
         _rb.velocity = -deathDirection;
      }
      Debug.DrawRay(previousChild.transform.position, -deathDirection * 100, Color.magenta);
      
   }
   
   private void LateUpdate()
   {
      if (mode != EnemyMode.Dead)
      {
         previousPosition = currentPosition;
         _previousY = _currentY;
      }
   }
}
