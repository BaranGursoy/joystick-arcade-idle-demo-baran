using System.Collections.Generic;
using UnityEngine;

public class BloodParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem bloodParticleSystem;
    [SerializeField] private GameObject bloodSplatPrefab;
    private List<ParticleCollisionEvent> _collisionEvents = new();

    public void Play()
    {
        bloodParticleSystem.Play();
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
        GameObject bloodSpatObject = ObjectPooler.Instance.SpawnFromPool(PrefabType.BloodSplat, contactPoint, bloodSplatPrefab.transform.rotation);
        bloodSpatObject.transform.SetParent(null);
        GameActions.BloodSplatCreated?.Invoke();
    }
}