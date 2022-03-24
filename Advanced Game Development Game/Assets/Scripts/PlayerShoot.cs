using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bulletSource;
    [SerializeField] private Transform gun;
    [SerializeField] private Animator[] muzzleFlashes;
    [SerializeField] private LayerMask ignoreLayers;
    private BulletChooser _bulletChooser;
    private LineRenderer lr;
    
    private Transform mainCam;
    private PlayerStats stats;
    
    private void Start()
    {
        mainCam = Camera.main.transform;
        stats = transform.root.GetComponent<PlayerStats>();
        gun.GetComponent<Collider>().enabled = false;
        lr = gun.GetComponent<LineRenderer>();
        _bulletChooser = FindObjectOfType<BulletChooser>();
    }

    private void Update()
    {
        bullet = _bulletChooser.currentBullet;
        
        if (stats.IsDead())
        {
            gun.transform.parent = null;
            gun.GetComponent<Rigidbody>().isKinematic = false;
            gun.GetComponent<Collider>().enabled = true;
        }

        Vector3 dir = CalculateDirection();
        lr.SetPosition(0, gun.position);
        lr.SetPosition(1, dir * 2000);


    }

    public void Shoot()
    {
        foreach (var j in muzzleFlashes)
        {
            j.SetTrigger("Flash");
        }

        Vector3 shootDir = CalculateDirection();
        
        GameObject instBullet = Instantiate(bullet, bulletSource.transform.position, Quaternion.identity);
        instBullet.GetComponent<Rigidbody>().velocity =
            shootDir * 120 * instBullet.GetComponent<BulletBehavior>().speed;
    }

    private Vector3 CalculateDirection()
    {
        float x = Screen.width / 2f;
        float y = Screen.height / 2f;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));
        Vector3 shootDir = ray.direction.normalized;
        
        RaycastHit hit;
        Physics.Raycast(mainCam.position, mainCam.forward, out hit, 1000, ignoreLayers);
        
        if (hit.collider)
            shootDir = (hit.point - bulletSource.transform.position).normalized;

        return shootDir;
    }
    
     

}
