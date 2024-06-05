using System;
using System.Collections;
using UnityEngine;

public class Mine : Interactable
{
    [SerializeField] private MineRespawner mineRespawner;

    [SerializeField] private int mineHealth = 7;
    [SerializeField] private float mineRespawnTime = 3f;

    private int _mineStartingHealth;
    private bool _isMineFinished = false;

    private void Start()
    {
        _mineStartingHealth = mineHealth;
    }

    private void Update()
    {
        if(_isMineFinished) return;
        
        if(!isPlayerInsideArea) return;

        if (passedTimeBetweenCollectibleSpawns >= collectibleSpawnRate)
        {
            Collectible spawnedCollectible = SpawnCollectible(transform);

            DecreaseMineHealth();
            
            GameActions.PlaySfxAction?.Invoke(SFXType.Mine);
            
            if(playerController.StackIsEmpty || (playerController.StackHasEmptySpace() && playerController.PeekStack().GetType() == typeof(Ore)))
            {
                AddToPlayerStack();
                SendCollectibleToPlayerStack();
            }

            else if (spawnedCollectible is Ore spawnedOre)
            {
                SendOreToRandomPlace(spawnedOre);
            }
            
            ResetPassedTime();
        }
        
        passedTimeBetweenCollectibleSpawns += Time.deltaTime;
    }

    private void DecreaseMineHealth()
    {
        mineHealth--;

        if (mineHealth <= 0)
        {
            playerController.DisablePickaxe();
            _isMineFinished = true;
            RespawnMine();
        }
    }

    private void RespawnMine()
    {
        mineRespawner.RespawnMine(mineRespawnTime, this);
    }

    private void SendOreToRandomPlace(Ore spawnedOre)
    {
        spawnedOre.SendToOreToRandomPlace(transform.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!playerController)
            {
                playerController = other.gameObject.GetComponentInParent<PlayerController>();
            }

            if (playerController)
            {
                playerController.ActivateAndSwingPickaxe(collectibleSpawnRate);
                playerController.SetCollectibleHeight(collectiblePrefab.transform);
            }
            
            isPlayerInsideArea = true;
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        isPlayerInsideArea = false;
        ResetPassedTime();

        if (playerController)
        {
            playerController.DisablePickaxe();
        }
    }

    public void ResetMine()
    {
        if (isPlayerInsideArea)
        {
            playerController.ActivateAndSwingPickaxe(collectibleSpawnRate);
        }
        
        mineHealth = _mineStartingHealth;
        _isMineFinished = false;
    }
}
