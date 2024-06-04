using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private Transform stackStartPointTransform;
    [SerializeField] private InputActionReference moveAction;

    [SerializeField] private Transform collectibleOreTransform;

    private float _collectibleOreHeight;
    private int _stackObjectCount;
    
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
        _collectibleOreHeight = collectibleOreTransform.GetChild(0).localScale.y;
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
            stackStartPointTransform.localPosition + (Vector3.up * (_collectibleOreHeight * _stackObjectCount));
        
        _stackObjectCount++;
        
        return nextStackItemLocalPosition;
    }
}
