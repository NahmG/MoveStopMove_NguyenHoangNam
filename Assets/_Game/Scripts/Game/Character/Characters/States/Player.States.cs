using Core;
using UnityEngine;

public class PlayerIdleState : IdleState
{
    public PlayerIdleState(CoreSystem core) : base(core)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Core.MOVEMENT.SetVelocity(Vector3.zero);
    }

    public override void Update()
    {
        base.Update();

        if (Core.NAVIGATION.MoveDirection.sqrMagnitude > .01f)
        {
            ChangeState(STATE.MOVE);
        }
        else if (Target != null && !IsAttackCooldown)
        {
            ChangeState(STATE.ATTACK);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Core.MOVEMENT.SetVelocity(Vector3.zero);
    }
}

public class PlayerMoveState : MoveState
{
    public PlayerMoveState(CoreSystem core) : base(core)
    {
    }

    public override void Update()
    {
        base.Update();
        if (Core.NAVIGATION.MoveDirection.sqrMagnitude < .01f)
        {
            ChangeState(STATE.IDLE);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Core.NAVIGATION.MoveDirection.sqrMagnitude < .01f) return;

        Vector3 move = Core.NAVIGATION.MoveDirection;
        move.y = 0;

        Core.DISPLAY.SetSkinRotation(Quaternion.LookRotation(move), true);
        Core.MOVEMENT.SetVelocity(move * Core.Stats.Speed.Value);
    }

    public override void Exit()
    {
    }
}

public class PlayerInAirState : InAirState
{
    public PlayerInAirState(CoreSystem core) : base(core)
    {
    }
}

public class PlayerAttackState : AttackState
{
    public PlayerAttackState(CoreSystem core) : base(core)
    {
    }

    public override void Update()
    {
        if (Core.NAVIGATION.MoveDirection.sqrMagnitude > .01f)
        {
            ChangeState(STATE.MOVE);
        }
        base.Update();
    }

    protected override void RotateTowardTarget()
    {
        base.RotateTowardTarget();

        Vector3 dir = Core.SENSOR.TargetDir;
        Core.DISPLAY.SetSkinRotation(Quaternion.LookRotation(dir), true);
    }
}

public class PlayerDeadState : DeadState
{
    public PlayerDeadState(CoreSystem core) : base(core)
    {
    }

    public override void OnDeath()
    {
        base.OnDeath();
        GameplayManager.Ins.OnGameEnd(false);
    }
}

public class PlayerWinState : BaseLogicState
{
    public override STATE Id => STATE.WIN;
    public PlayerWinState(CoreSystem core) : base(core)
    {
    }

    public override void Enter()
    {
        Core.DISPLAY.ChangeAnim(CONSTANTS.WIN_ANIM_NAME);
        Core.MOVEMENT.StopMovement();
        Core.DISPLAY.SetSkinRotation(new Vector3(0, 180, 0), true);

        GameplayManager.Ins.OnGameEnd(true);
        ((Player)_char).OnDespawn();
    }
}

public class PlayerShopSkin : BaseLogicState
{
    public override STATE Id => STATE.SHOP_SKIN;

    public PlayerShopSkin(CoreSystem core) : base(core)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Core.DISPLAY.ChangeAnim(CONSTANTS.SHOP_SKIN_ANIM_NAME);
    }
}

