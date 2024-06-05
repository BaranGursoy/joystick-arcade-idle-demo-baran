using DG.Tweening;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    public void SendCollectibleToPlayer(PlayerController playerController)
    {
        transform.SetParent(playerController.StackStartPointTransform, true);
        transform.localScale = Vector3.one;
        transform.DOLocalMove(playerController.GetNextStackItemPosition(), 0.3f)
            .SetEase(Ease.OutSine);

        transform.DOLocalRotate(playerController.StackStartPointTransform.localRotation.eulerAngles, 0.3f);
    }
    
    public void SendCollectibleToMachine(Interactable interactableMachine)
    {
        transform.DOJump(interactableMachine.transform.position, 0.7f, 1, 0.3f)
            .SetEase(Ease.OutSine).OnComplete(()=>
            {
                interactableMachine.CollectableArrived();
                Destroy(gameObject);
            });

        transform.DORotate(interactableMachine.transform.rotation.eulerAngles, 0.29f);
    }
    
    public abstract void OnCollect();

}
