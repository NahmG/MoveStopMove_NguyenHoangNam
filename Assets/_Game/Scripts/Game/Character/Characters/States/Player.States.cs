using Core;
using UnityEngine;

public class PlayerIdleState : IdleState
{
    public PlayerIdleState(CoreSystem core) : base(core)
    {
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
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

        Vector3 move = Core.NAVIGATION.MoveDirection;
        move.y = 0;

        Core.DISPLAY.SetSkinRotation(Quaternion.LookRotation(move), true);
        Core.MOVEMENT.SetVelocity(move * Core.Stats.Speed.Value);

        if (Core.SENSOR.IsGoUpBridge)
            Core.MOVEMENT.ApplyGravity(0);
        else
            Core.MOVEMENT.ApplyGravity(100);
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

    public override STATE Id => STATE.IN_AIR;

    public override void Exit()
    {
    }
}

public class PlayerDeadState : DeadState
{
    public PlayerDeadState(CoreSystem core) : base(core)
    {
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
    }
}

