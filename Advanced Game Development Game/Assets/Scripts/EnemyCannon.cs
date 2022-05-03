using UnityEngine;

public class EnemyCannon : Enemy
{
    [Header("Turret Variables")] public Transform swivelBase;

    public Transform tiltCannon;
    public GameObject sparkObject;
    public float turnSpeed;
    public Vector2 scaleRange;
    public float multiplier;
    public LineRenderer line;
    public Transform lineSource;
    public float viewAngle;
    public Transform laserParticle;

    protected override void StandardMovement()
    {
        bool lineOfSight = false;
        Vector3 dir = (target.position - lineSource.position);
        Debug.DrawRay(lineSource.position, dir * 100);
        if (Physics.Raycast(lineSource.position, dir, out RaycastHit hit, 100))
            if (hit.collider.CompareTag("Player"))
                lineOfSight = true;

        if (PlayerWithinAngle() && lineOfSight)
        {
            line.enabled = true;
            FaceTarget(target.position);
            FireLaser();
        }
        else
        {
            line.enabled = false;
            laserParticle.transform.position = Vector3.one * 1000000;
        }
    }

    protected override void SpecificDeath()
    {
        line.enabled = false;
        laserParticle.transform.position = Vector3.one * 1000000;
    }

    private bool PlayerWithinAngle()
    {
        Vector3 targetOffset = target.position;
        targetOffset.y += 0.5f;
        Vector3 dir = (targetOffset - lineSource.position).normalized;
        float angle = Vector3.Angle(dir, lineSource.forward);
        return angle < viewAngle;
    }

    private void FireLaser()
    {
        line.SetPosition(0, lineSource.position);
        RaycastHit? hitData = LaserInfo();

        if (hitData.HasValue)
        {
            line.SetPosition(1, hitData.Value.point);
            if (hitData.Value.collider.CompareTag("Player"))
                target.GetComponent<PlayerStats>().ChangeHealth(-5 * Time.deltaTime, false);

            Vector3 targetRotation = hitData.Value.normal;
            targetRotation.x -= 90;
            laserParticle.eulerAngles = targetRotation;
            laserParticle.transform.position = hitData.Value.point;
        }
        else
        {
            line.SetPosition(1, lineSource.forward * 1000);
        }
    }

    private RaycastHit? LaserInfo()
    {
        if (Physics.Raycast(lineSource.position, lineSource.forward, out RaycastHit hit, 1000)) return hit;

        return null;
    }

    private void FaceTarget(Vector3 targetPosition)
    {
        Vector3 playerDirection = (targetPosition - transform.position).normalized;
        Vector3 swivelDir = new Vector3(playerDirection.x, 0, playerDirection.z);
        Vector3 tiltDir = Vector3.right * playerDirection.y * multiplier;
        Quaternion targetRot = Quaternion.Euler(tiltDir);

        tiltCannon.localRotation =
            Quaternion.Lerp(tiltCannon.localRotation, targetRot, turnSpeed * Time.deltaTime);

        Quaternion swivelRotTarget = Quaternion.LookRotation(-swivelDir, Vector3.up);
        swivelBase.rotation = Quaternion.Lerp(swivelBase.rotation, swivelRotTarget,
            turnSpeed * Time.deltaTime);
    }
}