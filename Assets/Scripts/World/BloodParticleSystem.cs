using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BloodParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem bloodParticleSystem;
    [SerializeField] private GameObject bloodSplatPrefab;
    private List<ParticleCollisionEvent> _collisionEvents = new();

    private int _collisionCount;


    public void Play()
    {
        bloodParticleSystem.Play();
        _collisionCount = 0;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!other.gameObject.CompareTag("Floor")) return;
        if (!bloodParticleSystem) return;

        int numCollisionEvents = bloodParticleSystem.GetCollisionEvents(other, _collisionEvents);
        
        int i = 0;
        while (i < numCollisionEvents)
        {
            Vector3 pos = _collisionEvents[i].intersection;
            SpawnBloodSplat(pos);
            i++;
        }
    }

    private void SpawnBloodSplat(Vector3 contactPoint)
    {
        contactPoint.y = 0.1f;
        Instantiate(bloodSplatPrefab, contactPoint, bloodSplatPrefab.transform.rotation);
        GameActions.BloodSplatCreated?.Invoke();
    }
}