using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected GameObject collectiblePrefab;
    [SerializeField] protected PrefabType collectiblePrefabType;
    [SerializeField] protected float collectibleSpawnRate = 0.5f;
    
    [SerializeField] protected int neededCollectibleCount = 5;
    protected int droppedCollectibleCount = 0;

    protected PlayerController playerController;

    protected float passedTimeBetweenCollectibleSpawns = 0f;
    
    protected bool isPlayerInsideArea = false;

    protected Collectible lastSpawnedCollectible;

    protected Collectible SpawnCollectible(Transform parentTransform)
    {
        lastSpawnedCollectible =
            ObjectPooler.Instance.SpawnFromPool(collectiblePrefabType, parentTransform.position, Quaternion.identity).GetComponent<Collectible>();//Instantiate(collectiblePrefab, parentTransform.position, Quaternion.identity, null).GetComponent<Collectible>();
        
        lastSpawnedCollectible.transform.SetParent(parentTransform);

        return lastSpawnedCollectible;
    }

    protected void AddToPlayerStack()
    {
        if (playerController)
        {
            playerController.AddToCollectibleStack(lastSpawnedCollectible);
        }
    }
    
    protected void SendCollectibleToPlayerStack()
    {
        if (!playerController)
        {
            Debug.LogError("Player controller is null!");
            return;
        }


        if (!playerController.StackIsEmpty && playerController.PeekStack().GetType() != lastSpawnedCollectible.GetType()) return;
        
        lastSpawnedCollectible.SendCollectibleToPlayer(playerController);
    }
    
    
    protected void ResetPassedTime()
    {
        passedTimeBetweenCollectibleSpawns = 0f;
    }

    public virtual void CollectableArrived()
    {
        GameActions.PlaySfxAction?.Invoke(SFXType.PutCollectible);
        droppedCollectibleCount++;
    }

    public void ResetDroppedCollectibleCount()
    {
        droppedCollectibleCount = 0;
    }
}
