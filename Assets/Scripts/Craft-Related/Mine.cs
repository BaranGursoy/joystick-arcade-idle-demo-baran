using UnityEngine;

public class Mine : Interactable
{
    [SerializeField] private MineRespawner mineRespawner;
    [SerializeField] private Transform mineParentTransform;

    [SerializeField] private int mineHealth = 7;
    [SerializeField] private float mineRespawnTime = 3f;

    private int _mineStartingHealth;
    private bool _isMineFinished = false;

    private float _scaleDecreaseFactor;

    private void Start()
    {
        _mineStartingHealth = mineHealth;
        _scaleDecreaseFactor = 1f / mineHealth;
    }

    private void Update()
    {
        if(_isMineFinished) return;
        
        if(!isPlayerInsideArea) return;

        if (passedTimeBetweenCollectibleSpawns >= collectibleSpawnRate)
        {
            SpawnOreFromMine();
        }
        
        passedTimeBetweenCollectibleSpawns += Time.deltaTime;
    }

    private void SpawnOreFromMine()
    {
        Collectible spawnedCollectible = SpawnCollectible(transform);

        AdjustScaleOfMine();

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

    private void AdjustScaleOfMine()
    {
        mineParentTransform.localScale -= (Vector3.one * _scaleDecreaseFactor);
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
        
        mineParentTransform.localScale = Vector3.one;
        
        mineHealth = _mineStartingHealth;
        _isMineFinished = false;
    }
}
