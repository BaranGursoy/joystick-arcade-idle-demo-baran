using DG.Tweening;
using UnityEngine;

public class BloodSplat : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float cleanDuration = 0.2f;
    private Material _material;
    private Color _originalColor;

    private void Awake()
    {
        _material = meshRenderer.material;
        _originalColor = _material.color;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Broom"))
        {
            CleanBloodSplat();
        }
    }

    private void CleanBloodSplat()
    {
        Color targetColor = new Color(_originalColor.r, _originalColor.g, _originalColor.b, _material.color.a - 0.5f);
        
        _material.DOColor(targetColor, cleanDuration).OnComplete(() =>
        {
            if (_material.color.a <= 0.2f)
            {
                GameActions.BloodSplatCleaned?.Invoke();
                DestroyBloodSplat();
            }
        });
    }

    private void DestroyBloodSplat()
    {
        ObjectPooler.Instance.ReturnToPool(gameObject, PrefabType.BloodSplat);
        _material.color = _originalColor;
    }
}
