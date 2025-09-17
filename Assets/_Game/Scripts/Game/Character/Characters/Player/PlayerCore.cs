using UnityEngine;

using Core;
public class PlayerCore : CoreSystem
{
    public override void Initialize(CharacterStats stats)
    {
        base.Initialize(stats);

        StateMachine.AddState(STATE.IDLE, new PlayerIdleState(this));
        StateMachine.AddState(STATE.MOVE, new PlayerMoveState(this));
        StateMachine.AddState(STATE.ATTACK, new PlayerAttackState(this));
        StateMachine.AddState(STATE.DEAD, new PlayerDeadState(this));
        StateMachine.AddState(STATE.WIN, new PlayerWinState(this));
        StateMachine.AddState(STATE.SHOP_SKIN, new PlayerShopSkin(this));
    }

    public override void Run()
    {
        base.Run();
        StateMachine.Start(STATE.IDLE);
    }

    public override void UpdateData()
    {
        base.UpdateData();
        StateMachine.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        StateMachine.FixedUpdate();
    }
}