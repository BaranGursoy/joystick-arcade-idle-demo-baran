using System;
using System.Collections;
using UnityEngine;

public class Processor : Interactable
{
    [SerializeField] private IngotHolder ingotHolder;
    private void SendOreToProcessor(Collectible collectible)
    {
        if (collectible is Ore ore)
        {
            ore.SendCollectibleToMachine(this);
        }
    }

    public override void CollectableArrived()
    {
        base.CollectableArrived();

        if (droppedCollectibleCount >= neededCollectibleCount)
        {
            StartCoroutine(IngotSpawnCoroutine());
            droppedCollectibleCount = 0;
        }
    }

    private IEnumerator IngotSpawnCoroutine()
    {
        float passedTimeForCoroutine = 0f;
        while (passedTimeForCoroutine < collectibleSpawnRate)
        {
            
            passedTimeForCoroutine += Time.deltaTime;
            yield return null;
        }
        
        Collectible spawnedCollectible = SpawnCollectible(transform);

        if (spawnedCollectible is Ingot spawnedIngot)
        {
            spawnedIngot.SendCollectibleToMachineStack(ingotHolder);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!playerController)
            {
                playerController = other.gameObject.GetComponentInParent<PlayerController>();
            }
            
            isPlayerInsideArea = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerController && !playerController.StackIsEmpty)
        {
            Collectible collectibleFromPlayer = playerController.TakeFromCollectibleStack();
            SendOreToProcessor(collectibleFromPlayer);
        }
    }
}
