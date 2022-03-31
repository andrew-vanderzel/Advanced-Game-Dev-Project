using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer ap;

    private void Start() => ap = this;

    public AudioClip shootSound;
    public AudioClip damageSound;
    public AudioClip enemyHitSound;
    public AudioClip jetpackSound;
    public AudioClip explosionSound;
    public AudioSource source;

    public void PlayShootSound() => PlaySound(shootSound);
    public void PlayDamageSound() => PlaySound(damageSound);
    public void PlayExplosionSound() => PlaySound(explosionSound);
    
    private void PlaySound(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
}
