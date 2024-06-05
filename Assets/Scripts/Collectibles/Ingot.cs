using DG.Tweening;

public class Ingot : Collectible
{
    public void SendIngotToMachineStack(IngotHolder ingotHolder)
    {
        transform.DOLocalJump(ingotHolder.GetNextStackItemPosition(), 0.7f, 1, 0.3f)
            .SetEase(Ease.OutSine).OnComplete((() =>
            {
                ingotHolder.AddToIngotStack(this);
                GameActions.PlaySfxAction?.Invoke(SFXType.IngotCrafted);
            }));

        transform.DOLocalRotate(ingotHolder.transform.localRotation.eulerAngles, 0.3f);
    }
}