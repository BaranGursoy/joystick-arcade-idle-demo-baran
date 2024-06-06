using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ore : Collectible
{
    private const float RandomPlaceRadius = 4f;
    private const float JumpPower = 1f;
    private const int NumberOfJumps = 1;
    private const float JumpDuration = 0.45f;
    
    private bool _isCollectible;
    
    public void SendOreToRandomPlace(Vector3 startPosition)
    {
        Vector3 endPos = startPosition + Random.onUnitSphere * RandomPlaceRadius;
        endPos.y = 0.1f;

        TweenAnimateUtils.JumpToPosition(transform, endPos, JumpPower, NumberOfJumps, JumpDuration, Ease.OutSine, () =>
        {
            GameActions.StopShakingCamera?.Invoke();
        });

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
