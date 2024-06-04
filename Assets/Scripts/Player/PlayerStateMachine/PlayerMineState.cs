using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMineState : PlayerMoveState
{
    public PlayerMineState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {}
    
    public override void Enter()
    {
        ActivatePickaxe();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void ActivatePickaxe()
    {
        
    }
}
