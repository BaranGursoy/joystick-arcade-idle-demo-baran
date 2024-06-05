using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBall : MonoBehaviour
{
    [SerializeField] private GameObject bloodSplatPrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            SpawnBloodSplat();
        }
    }

    private void SpawnBloodSplat()
    {
        Instantiate(bloodSplatPrefab, transform.position, bloodSplatPrefab.transform.rotation);
        Destroy(gameObject);
    }
}
