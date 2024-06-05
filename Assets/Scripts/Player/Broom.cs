using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : MonoBehaviour
{
    private int BloodSplatLayer;

    private void Awake()
    {
        BloodSplatLayer = LayerMask.NameToLayer("BloodSplat");
    }

    private void OnCollisionStay(Collision collision)
    {
        /*if (collision.gameObject.layer == BloodSplatLayer)
        {
            BloodSplat bloodSplat = collision.gameObject.GetComponent<BloodSplat>();
            if (bloodSplat != null)
            {
                foreach (ContactPoint contact in collision.contacts)
                {
                    bloodSplat.Clean(contact.point, 0.1f);
                }
            }
        }*/
    }
}
