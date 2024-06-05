using System.Collections;
using TMPro;
using UnityEngine;

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

        ResetDroppedCollectibleCount();
        UpdateRockCountText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HandlePlayerEnter(other);
        }
    }

    private void HandlePlayerEnter(Collider other)
    {
        if (!playerController)
        {
            playerController = other.gameObject.GetComponentInParent<PlayerController>();
        }

        if (!playerController) return;

        playerController.SetCollectibleHeight(collectiblePrefab.transform);
        isPlayerInsideArea = true;

        if (!playerController.StackIsEmpty)
        {
            StartCoroutine(SendPlayerOresCoroutine());
        }
    }

    private IEnumerator SendPlayerOresCoroutine()
    {
        if (droppedCollectibleCount >= neededCollectibleCount) yield return null;

        int neededCollectibleLeft = neededCollectibleCount - droppedCollectibleCount;
        
        for (int i = 0; i < neededCollectibleLeft; i++)
        {
            if (playerController.StackIsEmpty || playerController.PeekStack() is not Ore) yield break;

            Collectible collectibleFromPlayer = playerController.TakeFromCollectibleStack();
            SendOreToProcessor(collectibleFromPlayer);

            yield return new WaitForSeconds(0.1f);
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
