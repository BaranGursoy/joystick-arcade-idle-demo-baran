using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private BloodParticleSystem _bloodParticleSystem;
    [SerializeField] private Rigidbody _enemyRb;
    [SerializeField] private float knockbackForce = 3f;
    [SerializeField] private float verticalKnockbackForce = 6f;
    [SerializeField] private GameObject enemyTutorial;
    private int _health = 5;

    private void GetHit(Vector3 swordPos)
    {
        GameActions.ShakeCamera?.Invoke();
        _health--;
        SplashBlood();
        
        GameActions.PlayEnemyHitSfx?.Invoke();

        if (_health <= 0)
        {
            DestroyEnemy();
            return;
        }

        KnockBackEnemy(swordPos);
    }

    private void KnockBackEnemy(Vector3 swordPos)
    {
        Vector3 forceDirection = (transform.position - swordPos).normalized;
        forceDirection.y = 0; 
        _enemyRb.AddForce(forceDirection * knockbackForce + Vector3.up * verticalKnockbackForce, ForceMode.Impulse);
    }

    private void DestroyEnemy()
    {
        GameActions.EnemyDied?.Invoke();
        GameActions.StopShakingCamera?.Invoke();
        _bloodParticleSystem.transform.SetParent(null);
        enemyTutorial.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void SplashBlood()
    {
        _bloodParticleSystem.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            GetHit(other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            GameActions.StopShakingCamera?.Invoke();
        }
    }
}