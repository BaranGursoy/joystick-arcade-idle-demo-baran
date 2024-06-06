using DG.Tweening;
using UnityEngine;

public static class TweenAnimateUtils
{
    public static void SetParentAndPreserveScale(Transform child, Transform newParent)
    {
        Vector3 originalGlobalScale = child.lossyScale;
        child.SetParent(newParent, true);
        
        Vector3 parentGlobalScale = newParent.lossyScale;
        child.localScale = new Vector3(
            originalGlobalScale.x / parentGlobalScale.x,
            originalGlobalScale.y / parentGlobalScale.y,
            originalGlobalScale.z / parentGlobalScale.z
        );
    }

    public static void JumpToPosition(Transform transform, Vector3 targetPosition, float jumpPower, int numJumps, float duration, Ease ease, System.Action onComplete = null)
    {
        transform.DOJump(targetPosition, jumpPower, numJumps, duration).SetEase(ease).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }
    
    public static void JumpToLocalPosition(Transform transform, Transform target, Vector3 localPosition, float jumpPower, int numJumps, float duration, Ease ease, System.Action onComplete = null)
    {
        SetParentAndPreserveScale(transform, target);
        transform.DOLocalJump(localPosition, jumpPower, numJumps, duration).SetEase(ease).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }
    
    public static void MoveToLocalPosition(Transform transform, Transform target, Vector3 localPosition, float duration, Ease ease, System.Action onComplete = null)
    {
        SetParentAndPreserveScale(transform, target);

        transform.DOLocalMove(localPosition, duration).SetEase(ease).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
        transform.DOLocalRotate(target.localRotation.eulerAngles, duration, RotateMode.Fast);
    }
    
    
    public static void MoveToPosition(Transform transform, Transform target, Vector3 position, float duration, Ease ease, System.Action onComplete = null)
    {
        SetParentAndPreserveScale(transform, target);

        transform.DOMove(position, duration).SetEase(ease).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
        transform.DOLocalRotate(target.localRotation.eulerAngles, duration, RotateMode.Fast);
    }
}