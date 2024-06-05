using DG.Tweening;
using UnityEngine;

public class Broom : MonoBehaviour
{
    private Tween _broomTween;
    
    public void ActivateAndSwoopBroom(float oneSwingAndBackDuration)
    {
        Vector3 startRotation = new Vector3(0f, 0f, -80f);
        transform.localRotation = Quaternion.Euler(startRotation);
        gameObject.SetActive(true);

        Vector3 targetRotationForBroom = new Vector3(0f, 0f, 80f);

        float oneSwingDuration = oneSwingAndBackDuration / 2f;

        _broomTween = transform.DOLocalRotate(targetRotationForBroom, oneSwingDuration).SetEase(Ease.OutSine)
            .SetLoops(-1, LoopType.Yoyo).OnStepComplete(() =>
            {
                GameActions.PlaySfxAction?.Invoke(SFXType.BroomSweep);
            });
    }

    public void DisableBroom()
    {
        _broomTween.Pause();
        gameObject.SetActive(false);
    }
}
