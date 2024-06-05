using System;
using UnityEngine;

public class Mine : Interactable
{
    private void Update()
    {
        if(!isPlayerInsideArea) return;

        if (passedTimeBetweenCollectibleSpawns >= collectibleSpawnRate)
        {
            Collectible spawnedCollectible = SpawnCollectible(transform);
            
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
    
}
