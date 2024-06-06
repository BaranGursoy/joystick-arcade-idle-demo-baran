using UnityEngine;

public static class PlayerUtils
{
    public static void HandlePlayerEnter(Collider other, ref PlayerController playerController, Transform collectiblePrefab, ref bool isPlayerInsideArea)
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
                isPlayerInsideArea = true;
            }
        }
    }

    public static void HandlePlayerExit(Collider other, ref bool isPlayerInsideArea, PlayerController playerController)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInsideArea = false;
            playerController.DisablePickaxe();
        }
    }
}