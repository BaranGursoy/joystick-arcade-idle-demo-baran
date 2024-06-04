using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngotHolder : MonoBehaviour
{
    [SerializeField] private Transform ingotTransform;
    
    private Stack<Ingot> _ingotStack = new Stack<Ingot>();
    
    private float _ingotHeight;

    private void Start()
    {
        Transform actualIngotTransform = ingotTransform.GetChild(0);
        MeshRenderer actualIngotMeshRenderer = actualIngotTransform.GetComponent<MeshRenderer>();

        _ingotHeight = actualIngotMeshRenderer.bounds.size.y;
    }

    public void AddToIngotStack(Ingot ingot)
    {
        _ingotStack.Push(ingot);
    }
    
    public void SendIngotsToPlayer()
    {
        
    }

    public Vector3 GetNextStackItemPosition()
    {
        Vector3 nextStackItemLocalPosition =
            transform.localPosition + (Vector3.up * (_ingotHeight * _ingotStack.Count)) + (Vector3.up * 0.1f);

        return nextStackItemLocalPosition;
    }
}
