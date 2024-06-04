using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private GameObject orePrefab;
    [SerializeField] private float oreSpawnRate = 0.5f;

    private PlayerController _playerController;

    private float _passedTimeBetweenOreSpawns = 0f;
    
    private bool _isPlayerInsideArea = false;

    private Ore _lastSpawnedOre;

    private void Update()
    {
        if(!_isPlayerInsideArea) return;

        if (_passedTimeBetweenOreSpawns >= oreSpawnRate)
        {
            SpawnOre();
            SendOreToPlayerStack();
            ResetPassedTime();
        }
        
        _passedTimeBetweenOreSpawns += Time.deltaTime;
    }
    
    private void SpawnOre()
    {
        _lastSpawnedOre = Instantiate(orePrefab, transform.position, Quaternion.identity, transform).GetComponent<Ore>();
    }
    
    private void SendOreToPlayerStack()
    {
        if (!_playerController)
        {
            Debug.LogError("Player controller is null!");
            return;
        }

        _lastSpawnedOre.SendOreToPlayer(_playerController);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_playerController)
            {
                _playerController = other.gameObject.GetComponentInParent<PlayerController>();
            }
            
            _isPlayerInsideArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isPlayerInsideArea = false;
        ResetPassedTime();
    }

    private void ResetPassedTime()
    {
        _passedTimeBetweenOreSpawns = 0f;
    }
}
