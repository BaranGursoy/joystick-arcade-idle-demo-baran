using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHolder : MonoBehaviour
{
    private bool _swordCrafted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _swordCrafted)
        {
            GameActions.PlayerTouchedSword?.Invoke();
            gameObject.SetActive(false);
        }
    }


    public void SetSwordCrafted()
    {
        _swordCrafted = true;
    }
}
