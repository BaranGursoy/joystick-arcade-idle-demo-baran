using UnityEngine;

public class SwordHolder : MonoBehaviour
{
    private bool _swordCrafted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _swordCrafted)
        {
            GameActions.PlayerTouchedSword?.Invoke();
            Destroy(transform.GetChild(0).gameObject);
            _swordCrafted = false;
        }
    }
    
    public void SetSwordCrafted()
    {
        _swordCrafted = true;
    }
}
