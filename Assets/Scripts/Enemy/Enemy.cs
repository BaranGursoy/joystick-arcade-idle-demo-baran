using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject bloodPrefab;

    private int _health = 5;

    private void GetHit()
    {
        _health--;
        SplashBlood();

        if (_health <= 0)
        {
            GameActions.EnemyDied?.Invoke();
            Destroy(gameObject);
        }
    }

    private void SplashBlood()
    {
        GameObject spawnedBlood = Instantiate(bloodPrefab, transform.position, Quaternion.identity);

        Vector3 bloodSplashPos = transform.position + (Random.onUnitSphere * Random.Range(3f, 5f));
        bloodSplashPos.y = 0.1f;

        spawnedBlood.transform.DOJump(bloodSplashPos, 3f, 1, 0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            GetHit();
        }
    }
}
