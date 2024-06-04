using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Processor : Interactable
{
    [SerializeField] private IngotHolder ingotHolder;
    [SerializeField] private TextMeshPro processorCountTMP;
    [SerializeField] private TextMeshPro processorTimerTMP;

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

        UpdateRockCountText();

        if (droppedCollectibleCount >= neededCollectibleCount)
        {
            StartCoroutine(IngotSpawnCoroutine());
        }
    }

    private IEnumerator IngotSpawnCoroutine()
    {
        ActivateProcessTimerTMP();
        
        float passedTimeForCoroutine = collectibleSpawnRate;
        while (passedTimeForCoroutine > 0f)
        {
            int timeForText = Mathf.CeilToInt(passedTimeForCoroutine);
            processorTimerTMP.text = timeForText.ToString();
            passedTimeForCoroutine -= Time.deltaTime;
            yield return null;
        }
        
        processorTimerTMP.text = "0";

        yield return new WaitForSeconds(0.1f);

        DisableProcessTimerTMP();
        
        Collectible spawnedCollectible = SpawnCollectible(transform);

        if (spawnedCollectible is Ingot spawnedIngot)
        {
            spawnedIngot.SendIngotToMachineStack(ingotHolder);
        }
        
        droppedCollectibleCount = 0;
        UpdateRockCountText();
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
                playerController.SetCollectibleHeight(collectiblePrefab.transform);
            }
            
            isPlayerInsideArea = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerController && !playerController.StackIsEmpty)
        {
            if(droppedCollectibleCount >= neededCollectibleCount) return;

            if (playerController.PeekStack() is Ore)
            {
                Collectible collectibleFromPlayer = playerController.TakeFromCollectibleStack();
                SendOreToProcessor(collectibleFromPlayer);
            }
        }
    }

    private void UpdateRockCountText()
    {
        processorCountTMP.text = $"Rock Count: {droppedCollectibleCount}/{neededCollectibleCount}";
    }

    public void DisableProcessTimerTMP()
    {
        processorTimerTMP.gameObject.SetActive(false);
    }
    
    private void ActivateProcessTimerTMP()
    {
        processorTimerTMP.gameObject.SetActive(true);
    }
}
