using System.Collections;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource enemyAudioSource;

    private void OnEnable()
    {
        GameActions.PlayEnemyHitSfx += PlayEnemyHitSFX;
        GameActions.EnemyDied += AdjustEnemyAudioControllerAfterEnemyDies;
    }

    private void OnDestroy()
    {
        GameActions.PlayEnemyHitSfx -= PlayEnemyHitSFX;
        GameActions.EnemyDied -= AdjustEnemyAudioControllerAfterEnemyDies;
    }

    private void PlayEnemyHitSFX()
    {
        enemyAudioSource.Play();
    }

    private void AdjustEnemyAudioControllerAfterEnemyDies()
    {
        transform.SetParent(null);
        StartCoroutine(DeleteEnemyAudioControllerCoroutine());
    }

    private IEnumerator DeleteEnemyAudioControllerCoroutine()
    {
        yield return new WaitUntil(() => !enemyAudioSource.isPlaying);
        Destroy(gameObject);
    }
}
