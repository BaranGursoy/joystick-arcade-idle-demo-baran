using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private BloodParticleSystem _bloodParticleSystem;
    private int _health = 5;

    private void GetHit()
    {
        _health--;
        SplashBlood();

        if (_health <= 0)
        {
            GameActions.EnemyDied?.Invoke();
            _bloodParticleSystem.transform.SetParent(null);
            Destroy(gameObject);
        }
    }

    private void SplashBlood()
    {
        _bloodParticleSystem.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            GetHit();
        }
    }
}
