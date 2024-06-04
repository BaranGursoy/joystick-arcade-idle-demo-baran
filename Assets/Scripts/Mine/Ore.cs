using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public void SendOreToPlayer(PlayerController playerController)
    {
        transform.SetParent(playerController.StackStartPointTransform, true);
        transform.localScale = Vector3.one;
        transform.DOLocalMove(playerController.GetNextStackItemPosition(), 0.3f)
            .SetEase(Ease.OutSine);

        transform.DOLocalRotate(playerController.StackStartPointTransform.localRotation.eulerAngles, 0.3f);
    }
}
