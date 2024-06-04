using System;
using UnityEngine;

public class Mine : Interactable
{
    private void Update()
    {
        if(!isPlayerInsideArea) return;

        if (passedTimeBetweenCollectibleSpawns >= collectibleSpawnRate && playerController && playerController.StackHasEmptySpace())
        {
            SpawnCollectible(transform);
            AddToPlayerStack();
            SendCollectibleToPlayerStack();
            ResetPassedTime();
        }

        /*if (!playerController.StackHasEmptySpace() && playerController.IsPickaxeActive)
        {
            playerController.DisablePickaxe();
        }*/

        passedTimeBetweenCollectibleSpawns += Time.deltaTime;
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
