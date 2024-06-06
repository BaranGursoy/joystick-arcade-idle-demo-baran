using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Forge : Interactable
{
    [SerializeField] private Transform swordHolderTransform;
    [SerializeField] private TextMeshPro forgeIngotCountTMP;
    [SerializeField] private TextMeshPro forgeTimerTMP;
    [SerializeField] private SwordHolder swordHolder;
    
    private const float SwordSpawnDelay = 0.1f;
    private const float JumpPower = 0.7f;
    private const int NumberOfJumps = 1;
    private const float JumpDuration = 0.3f;

    private void SendIngotToProcessor(Collectible collectible)
    {
        if (collectible is Ingot ingot)
        {
            ingot.SendCollectibleToMachine(this);
        }
    }

    public override void CollectableArrived()
    {
        base.CollectableArrived();

        UpdateIngotCountText();

        if (droppedCollectibleCount >= neededCollectibleCount)
        {
            StartCoroutine(SwordSpawnCoroutine());
        }
    }

    private IEnumerator SwordSpawnCoroutine()
    {
        ActivateProcessTimerTMP();
        
        float passedTimeForCoroutine = collectibleSpawnRate;
        while (passedTimeForCoroutine > 0f)
        {
            int timeForText = Mathf.CeilToInt(passedTimeForCoroutine);
            forgeTimerTMP.text = timeForText.ToString();
            passedTimeForCoroutine -= Time.deltaTime;
            yield return null;
        }
        
        forgeTimerTMP.text = "0";

        yield return new WaitForSeconds(SwordSpawnDelay);

        DisableProcessTimerTMP();

        GameObject spawnedSword = Instantiate(collectiblePrefab, transform.localPosition, Quaternion.identity, null);
        spawnedSword.transform.SetParent(transform);

        MoveSwordToRightSide(spawnedSword);
        
        ResetDroppedCollectibleCount();
        UpdateIngotCountText();
    }

    private void MoveSwordToRightSide(GameObject spawnedSword)
    {
        spawnedSword.transform.SetParent(swordHolderTransform, true);
        TweenAnimateUtils.JumpToLocalPosition(spawnedSword.transform, swordHolderTransform, Vector3.zero, JumpPower, NumberOfJumps, JumpDuration, Ease.OutSine,
            () =>
            {
                swordHolder.SetSwordCrafted();
                GameActions.PlaySfxAction?.Invoke(SFXType.SwordCrafted);
            });
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
                HandlePlayerEnter();
            }
            
            isPlayerInsideArea = true;
        }
    } 
    
    private void HandlePlayerEnter()
    {
        if (playerController && !playerController.StackIsEmpty)
        {
            if(droppedCollectibleCount >= neededCollectibleCount) return;

            StartCoroutine(SendPlayerIngotsCoroutine());
        }
    }
    
    private IEnumerator SendPlayerIngotsCoroutine()
    {
        if (droppedCollectibleCount >= neededCollectibleCount) yield return null;

        int neededCollectibleLeft = neededCollectibleCount - droppedCollectibleCount;
        
        for (int i = 0; i < neededCollectibleLeft; i++)
        {
            if (playerController.StackIsEmpty || playerController.PeekStack() is not Ingot) yield break;

            Collectible collectibleFromPlayer = playerController.TakeFromCollectibleStack();
            SendIngotToProcessor(collectibleFromPlayer);

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void UpdateIngotCountText()
    {
        forgeIngotCountTMP.text = $"Ingot Count: {droppedCollectibleCount}/{neededCollectibleCount}";
    }

    private void DisableProcessTimerTMP()
    {
        forgeTimerTMP.gameObject.SetActive(false);
    }
    
    private void ActivateProcessTimerTMP()
    {
        forgeTimerTMP.gameObject.SetActive(true);
    }
}
