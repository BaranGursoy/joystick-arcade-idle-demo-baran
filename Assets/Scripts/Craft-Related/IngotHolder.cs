using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngotHolder : MonoBehaviour
{
    [SerializeField] private Transform ingotTransform;

    private Stack<Ingot> _ingotStack = new Stack<Ingot>();
    
    private float _ingotHeight;

    private PlayerController _playerController;

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
    
    private void SendIngotsToPlayer()
    {
        if(!_playerController.StackIsEmpty && _playerController.PeekStack() is not Ingot) return;
        
        StartCoroutine(SendIngotsToPlayerCoroutine());
    }

    private IEnumerator SendIngotsToPlayerCoroutine()
    {
        int stackCount = _ingotStack.Count;
        for (int i = 0; i < stackCount; i++)
        {
            Ingot poppedIngot = _ingotStack.Pop();
            _playerController.AddToCollectibleStack(poppedIngot);
            poppedIngot.SendCollectibleToPlayer(_playerController);
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    public Vector3 GetNextStackItemPosition()
    {
        Vector3 nextStackItemLocalPosition =
            transform.localPosition + (Vector3.up * (_ingotHeight * _ingotStack.Count)) + (Vector3.up * 0.1f);

        return nextStackItemLocalPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (!_playerController)
            { 
                _playerController = other.gameObject.GetComponentInParent<PlayerController>();
            }

            if (_playerController)
            {
                SendIngotsToPlayer();
            }
        }
    }
}
