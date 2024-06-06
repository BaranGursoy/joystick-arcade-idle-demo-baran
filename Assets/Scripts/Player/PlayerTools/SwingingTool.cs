using DG.Tweening;
using UnityEngine;

public abstract class SwingingTool : MonoBehaviour
{
    private Tween _swingTween;

    protected void ActivateAndSwing(Vector3 startRotation, Vector3 targetRotation, float oneSwingAndBackDuration, SFXType? sfxType)
    {
        transform.localRotation = Quaternion.Euler(startRotation);
        gameObject.SetActive(true);

        float oneSwingDuration = oneSwingAndBackDuration / 2f;

        _swingTween = transform.DOLocalRotate(targetRotation, oneSwingDuration)
            .SetEase(Ease.OutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .OnStepComplete(() =>
            {
                if (sfxType != null)
                {
                    GameActions.PlaySfxAction?.Invoke(sfxType.Value);
                }
            });
    }

    public void Disable()
    {
        _swingTween?.Pause();
        gameObject.SetActive(false);
    }
}