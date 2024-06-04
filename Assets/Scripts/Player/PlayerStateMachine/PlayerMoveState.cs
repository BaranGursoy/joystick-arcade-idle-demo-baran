using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int RunSpeed = Animator.StringToHash("runSpeed");
    public PlayerMoveState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        player.Animator.SetBool(IsMoving, true);
    }

    public override void HandleInput()
    {
        
    }

    public override void UpdateLogic()
    {
        MovePlayer();
    }

    public override void UpdatePhysics()
    {
    }

    public override void Exit()
    {
        player.Animator.SetBool(IsMoving, false);
    }

    private void MovePlayer()
    {
        Vector2 inputValueForMovement = player.MoveAction.action.ReadValue<Vector2>();

        if (inputValueForMovement.magnitude == 0f)
        {
            stateMachine.ChangeState(player.IdleState);
            return;
        }
        
        Vector3 moveDirection = new Vector3(inputValueForMovement.x, 0f, inputValueForMovement.y);

        moveDirection = player.transform.TransformDirection(moveDirection);
        player.CharacterController.Move(moveDirection * (player.PlayerMoveSpeed * Time.deltaTime));
        
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        player.PlayerModelTransform.rotation = Quaternion.Slerp(player.PlayerModelTransform.rotation, targetRotation, 5f * Time.deltaTime);
        
        player.Animator.SetFloat(RunSpeed, moveDirection.magnitude);
    }
    
}