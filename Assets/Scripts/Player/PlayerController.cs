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
    [SerializeField] private InputActionReference moveAction;


    public float PlayerMoveSpeed => playerMoveSpeed;
    public Animator Animator => animator;
    public CharacterController CharacterController => characterController;
    public Transform PlayerModelTransform => playerModelTransform;
    public InputActionReference MoveAction => moveAction;
    
    private PlayerStateMachine StateMachine { get; set; }
    
    public PLayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PLayerIdleState(this, StateMachine);
        MoveState = new PlayerMoveState(this, StateMachine);
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
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

}
