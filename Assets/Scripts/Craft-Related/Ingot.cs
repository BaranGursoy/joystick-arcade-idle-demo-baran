using DG.Tweening;
using UnityEngine;

public class Ingot : Collectible
{
    public override void OnCollect()
    {
    }
    
    public void SendIngotToMachineStack(IngotHolder ingotHolder)
    {
        transform.DOLocalJump(ingotHolder.GetNextStackItemPosition(), 0.7f, 1, 0.3f)
            .SetEase(Ease.OutSine).OnComplete((() =>
            {
                ingotHolder.AddToIngotStack(this);
            }));

        transform.DOLocalRotate(ingotHolder.transform.localRotation.eulerAngles, 0.3f);
    }
}
