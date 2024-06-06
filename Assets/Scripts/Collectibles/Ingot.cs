using DG.Tweening;
using UnityEngine;

public class Ingot : Collectible
{
    public void SendIngotToMachineStack(IngotHolder ingotHolder)
    {
        Vector3 nextStackPosition = ingotHolder.GetNextStackItemPosition();
        TweenAnimateUtils.MoveToPosition(transform, ingotHolder.transform, nextStackPosition, 0.3f, Ease.OutSine, () =>
        {
            ingotHolder.AddToIngotStack(this);
            GameActions.PlaySfxAction?.Invoke(SFXType.IngotCrafted);
        });
    }
}