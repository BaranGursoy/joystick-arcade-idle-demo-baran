using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private int maxCarriedCollectibleCount;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private Transform stackStartPointTransform;
    [SerializeField] private InputActionReference moveAction;

    [SerializeField] private Transform collectibleOreTransform;
    
    [SerializeField] private Pickaxe pickaxe;

    private Stack<Collectible> _collectibleStack = new Stack<Collectible>();

    private float _collectibleOreHeight;

    public float PlayerMoveSpeed => playerMoveSpeed;
    public Animator Animator => animator;
    public CharacterController CharacterController => characterController;
    public Transform PlayerModelTransform => playerModelTransform;
    public Transform StackStartPointTransform => stackStartPointTransform;
    public InputActionReference MoveAction => moveAction;
    
    private PlayerStateMachine StateMachine { get; set; }
    
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    
    
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine);
        MoveState = new PlayerMoveState(this, StateMachine);
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
        Transform actualOreTransform = collectibleOreTransform.GetChild(0);

        MeshRenderer actualOreMeshRenderer = actualOreTransform.GetComponent<MeshRenderer>();

        _collectibleOreHeight = actualOreMeshRenderer.bounds.size.y;
    }

    private void Update()
    {
        StateMachine.CurrentState.HandleInput();
        StateMachine.CurrentState.UpdateLogic();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.UpdatePhysics();
    }

    public Vector3 GetNextStackItemPosition()
    {
        Vector3 nextStackItemLocalPosition =
            stackStartPointTransform.localPosition + (Vector3.up * (_collectibleOreHeight * _collectibleStack.Count - 1));

        return nextStackItemLocalPosition;
    }

    public void ActivateAndSwingPickaxe(float oneSwingDuration)
    {
        pickaxe.ActivateAndSwingPickaxe(oneSwingDuration);
    }
    
    public void DisablePickaxe()
    {
        pickaxe.DisablePickaxe();
    }

    public void AddToCollectibleStack(Collectible comingOre)
    {
        _collectibleStack.Push(comingOre);
    }

    public bool StackIsEmpty => _collectibleStack.Count == 0;

    public Collectible TakeFromCollectibleStack()
    {
       return _collectibleStack.Pop();
    }

    public bool StackHasEmptySpace()
    {
        return _collectibleStack.Count < maxCarriedCollectibleCount;
    }
}
