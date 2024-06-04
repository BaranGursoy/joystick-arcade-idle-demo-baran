using UnityEngine;

public class PLayerIdleState : PlayerState
{
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    public PLayerIdleState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        player.Animator.SetBool(IsMoving, false);
    }

    public override void HandleInput()
    {
        if (player.MoveAction.action.triggered)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }

    public override void UpdateLogic()
    {

    }

    public override void UpdatePhysics()
    {

    }

    public override void Exit()
    {
    }
}