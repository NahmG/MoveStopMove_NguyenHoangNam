using UnityEngine;

using Core;
public class PlayerCore : CoreSystem
{
    public override void Initialize(CharacterStats stats)
    {
        base.Initialize(stats);

        stateMachine = new StateMachine();
        stateMachine.AddState(STATE.IDLE, new PlayerIdleState(this));
        stateMachine.AddState(STATE.MOVE, new PlayerMoveState(this));
        stateMachine.AddState(STATE.IN_AIR, new PlayerInAirState(this));
        stateMachine.AddState(STATE.DEAD, new PlayerDeadState(this));

        stateMachine.Start(STATE.IDLE);
    }

    public override void UpdateData()
    {
        base.UpdateData();
        stateMachine.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        stateMachine.FixedUpdate();
    }
}