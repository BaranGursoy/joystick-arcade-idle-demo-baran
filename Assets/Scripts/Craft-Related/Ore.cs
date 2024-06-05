using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ore : Collectible
{
    private bool _isCollectible;
    public void SendToOreToRandomPlace(Vector3 startPosition)
    {
        Vector3 endPos = startPosition + Random.onUnitSphere * 4f;
        endPos.y = 0.1f;

        transform.DOJump(endPos, 1f, 1, 0.45f);
        
        _isCollectible = true;
    }
    
    public void SendOreFromGroundToPlayerStack(PlayerController playerController)
    {
        if (!_isCollectible) return;
        
        playerController.SetCollectibleHeight(transform);
        playerController.AddToCollectibleStack(this);
        SendCollectibleToPlayer(playerController);
    }
}
