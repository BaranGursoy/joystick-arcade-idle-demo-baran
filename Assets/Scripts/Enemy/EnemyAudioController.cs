using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource enemyAudioSource;

    private void OnEnable()
    {
        GameActions.PlayEnemyHitSfx += PlayEnemyHitSFX;
    }

    private void OnDestroy()
    {
        GameActions.PlayEnemyHitSfx -= PlayEnemyHitSFX;
    }

    private void PlayEnemyHitSFX()
    {
        enemyAudioSource.Play();
    }
}
