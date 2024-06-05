using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float swordSwingDuration;
    [SerializeField] private float broomSwoopDuration;
    [SerializeField] private int maxCarriedCollectibleCount;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private Transform stackStartPointTransform;
    [SerializeField] private InputActionReference moveAction;
    
    [SerializeField] private Pickaxe pickaxe;
    [SerializeField] private Sword sword;
    [SerializeField] private Broom broom;

    private Stack<Collectible> _collectibleStack = new Stack<Collectible>();

    private float _collectibleHeight;

    public float PlayerMoveSpeed => playerMoveSpeed;
    public Animator Animator => animator;
    public CharacterController CharacterController => characterController;
    public Transform PlayerModelTransform => playerModelTransform;
    public Transform StackStartPointTransform => stackStartPointTransform;
    public InputActionReference MoveAction => moveAction;
    
    private PlayerStateMachine StateMachine { get; set; }
    
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    
    public bool playerGotSword { get; private set; }
    public bool playerKilledEnemy { get; private set; }
    
    
    public bool StackIsEmpty => _collectibleStack.Count == 0;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine);
        MoveState = new PlayerMoveState(this, StateMachine);
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void OnEnable()
    {
        GameActions.PlayerTouchedSword += SetPlayerGotSword;
        GameActions.EnemyDied += DisableSword;
        GameActions.EnemyDied += PlayerKilledEnemy;
        GameActions.GameFinished += DisableBroom;
    }

    private void OnDestroy()
    {
        GameActions.PlayerTouchedSword -= SetPlayerGotSword;
        GameActions.EnemyDied -= DisableSword;
        GameActions.EnemyDied -= PlayerKilledEnemy;
        GameActions.GameFinished -= DisableBroom;
    }

    private void SetPlayerGotSword()
    {
        playerGotSword = true;
    }
    
    private void PlayerKilledEnemy()
    {
        playerKilledEnemy = true;
        ActivateAndSwooshBroom();
    }

    public void SetCollectibleHeight(Transform collectibleParentTransform)
    {
        Transform actualCollectibleTransform = collectibleParentTransform.GetChild(0);

        MeshRenderer actualCollectibleMeshRenderer = actualCollectibleTransform.GetComponent<MeshRenderer>();

        _collectibleHeight = actualCollectibleMeshRenderer.bounds.size.y;
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
            stackStartPointTransform.localPosition + (Vector3.up * (_collectibleHeight * _collectibleStack.Count - 1));

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
    
    public void ActivateAndSwingSword()
    {
        if (!playerGotSword) return;
        
        sword.ActivateAndSwingSword(swordSwingDuration);
    }
    
    public void DisableSword()
    {
        if (!playerGotSword) return;

        sword.DisableSword();
    }
    
    public void ActivateAndSwooshBroom()
    {
        if (!playerKilledEnemy) return;
        
        broom.ActivateAndSwoopBroom(broomSwoopDuration);
    }
    
    public void DisableBroom()
    {
        if (!playerKilledEnemy) return;

        broom.DisableBroom();
    }

    public void AddToCollectibleStack(Collectible comingCollectible)
    {
        if(!StackIsEmpty && PeekStack().GetType() != comingCollectible.GetType()) return;
        
        _collectibleStack.Push(comingCollectible);
    }
    
    public Collectible TakeFromCollectibleStack()
    {
       return _collectibleStack.Pop();
    }

    public bool StackHasEmptySpace()
    {
        return _collectibleStack.Count < maxCarriedCollectibleCount;
    }

    public Collectible PeekStack()
    {
        return _collectibleStack.Peek();
    }

    public void CollectOreFromGround(Ore oreFromGround)
    {
        if (!StackIsEmpty && PeekStack() is not Ore) return;
        
        if(!StackHasEmptySpace()) return;

        oreFromGround.SendOreFromGroundToPlayerStack(this);
    }
    
}
