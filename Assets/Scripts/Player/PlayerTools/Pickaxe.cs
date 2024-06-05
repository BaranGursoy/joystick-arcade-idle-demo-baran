using DG.Tweening;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    private Tween _pickAxeTween;
    public void ActivateAndSwingPickaxe(float oneSwingAndBackDuration)
    {
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        gameObject.SetActive(true);

        Vector3 targetRotationForPickaxe = new Vector3(90f, 0f, 0f);

        float oneSwingDuration = oneSwingAndBackDuration / 2f;

        _pickAxeTween = transform.DOLocalRotate(targetRotationForPickaxe, oneSwingDuration).SetLoops(-1, LoopType.Yoyo);
    }

    public void DisablePickaxe()
    {
        _pickAxeTween.Pause();
        gameObject.SetActive(false);
    }
}
