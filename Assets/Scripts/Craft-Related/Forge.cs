using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Forge : Interactable
{
    [SerializeField] private Transform swordHolderTransform;
    [SerializeField] private TextMeshPro forgeIngotCountTMP;
    [SerializeField] private TextMeshPro forgeTimerTMP;
    [SerializeField] private SwordHolder swordHolder;

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

        yield return new WaitForSeconds(0.1f);

        DisableProcessTimerTMP();

        GameObject spawnedSword = Instantiate(collectiblePrefab, transform.localPosition, Quaternion.identity, null);
        spawnedSword.transform.SetParent(transform);

        MoveSwordToRightSide(spawnedSword);
        
        UpdateIngotCountText();

        ResetDroppedCollectibleCount();
    }

    private void MoveSwordToRightSide(GameObject spawnedSword)
    {
        spawnedSword.transform.SetParent(swordHolderTransform, true);
        spawnedSword.transform.DOLocalJump(Vector3.zero, 0.7f, 1, 0.3f).SetEase(Ease.OutSine).OnComplete(() =>
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
            }
            
            isPlayerInsideArea = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerController && !playerController.StackIsEmpty)
        {
            if(droppedCollectibleCount >= neededCollectibleCount) return;

            if (playerController.PeekStack() is Ingot)
            {
                Collectible collectibleFromPlayer = playerController.TakeFromCollectibleStack();
                SendIngotToProcessor(collectibleFromPlayer);
            }
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
