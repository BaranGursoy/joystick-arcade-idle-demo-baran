public abstract class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerController player;

    public PlayerState(PlayerController player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public abstract void Enter();
    public abstract void HandleInput();
    public abstract void UpdateLogic();
    public abstract void UpdatePhysics();
    public abstract void Exit();
}