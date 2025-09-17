using Core;
using UnityEngine;

public class PlayerIdleState : IdleState
{
    public PlayerIdleState(CoreSystem core) : base(core)
    {
    }
}

public class PlayerMoveState : MoveState
{
    public PlayerMoveState(CoreSystem core) : base(core)
    {
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

    protected override void RotateTowardTarget()
    {
        base.RotateTowardTarget();

        ICharacter target = Core.SENSOR.Target;
        Vector3 dir = target.TF.position - _char.TF.position;
        dir.y = 0;

        Core.DISPLAY.SetSkinRotation(Quaternion.LookRotation(dir), true);
    }
}

public class PlayerDeadState : DeadState
{
    public PlayerDeadState(CoreSystem core) : base(core)
    {
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

