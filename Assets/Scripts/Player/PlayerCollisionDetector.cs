using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            playerController.ActivateAndSwingSword();
        }

        if (other.gameObject.CompareTag("Ore"))
        {
            Ore oreFromGround = other.gameObject.GetComponent<Ore>();
            playerController.CollectOreFromGround(oreFromGround);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            playerController.DisableSword();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnCollisionExit(Collision other)
    {
        
    }
}
