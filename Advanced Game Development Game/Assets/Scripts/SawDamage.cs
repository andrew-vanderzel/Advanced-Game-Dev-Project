using UnityEngine;

public class SawDamage : MonoBehaviour
{
    public float damage;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player"))
        {
            if(transform.root.GetComponent<EnemyStats>().Health > 0)
                other.GetComponent<PlayerStats>().ChangeHealth(-damage * Time.deltaTime); 
        }
    }
}
