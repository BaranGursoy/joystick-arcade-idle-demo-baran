using DG.Tweening;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    private const float MoveDuration = 0.3f;
    private const float JumpPower = 0.7f;
    private const int NumberOfJumps = 1;
    private const float JumpDuration = 0.3f;

    
    public void SendCollectibleToPlayer(PlayerController playerController)
    {
        Vector3 nextStackPosition = playerController.GetNextStackItemPosition();
        TweenAnimateUtils.MoveToLocalPosition(transform, playerController.StackStartPointTransform, nextStackPosition, MoveDuration, Ease.OutSine, () =>
        {
            GameActions.StopShakingCamera?.Invoke();
            GameActions.PlaySfxAction?.Invoke(SFXType.CollectItem);
        });
    }

    public void SendCollectibleToMachine(Interactable interactableMachine)
    {
        PrefabType collectibleType = this is Ingot ? PrefabType.Ingot : PrefabType.Ore;

        TweenAnimateUtils.JumpToPosition(transform, interactableMachine.transform.position, JumpPower, NumberOfJumps, JumpDuration, Ease.OutSine, () =>
        {
            interactableMachine.CollectableArrived();
            ObjectPooler.Instance.ReturnToPool(gameObject, collectibleType);
        });
    }
}