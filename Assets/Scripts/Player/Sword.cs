using System;
using DG.Tweening;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Tween _swordTween;
    
    public void ActivateAndSwingSword(float oneSwingAndBackDuration)
    {
        Vector3 startRotation = new Vector3(0f, -80f, 0f);
        transform.localRotation = Quaternion.Euler(startRotation);
        gameObject.SetActive(true);

        Vector3 targetRotationForPickaxe = new Vector3(0f, 80f, 0f);

        float oneSwingDuration = oneSwingAndBackDuration / 2f;

        _swordTween = transform.DOLocalRotate(targetRotationForPickaxe, oneSwingDuration).SetEase(Ease.OutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public void DisableSword()
    {
        _swordTween.Pause();
        gameObject.SetActive(false);
    }
}
