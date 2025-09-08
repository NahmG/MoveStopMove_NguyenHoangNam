using UnityEngine;

#region BASE STATE

using Core;

public abstract class BaseLogicState : BaseState
{
    protected CoreSystem Core;
    protected CharacterStats Stats;
    public BaseLogicState(CoreSystem core)
    {
        Core = core;
    }
}

public abstract class GroundedState : BaseLogicState
{
    protected GroundedState(CoreSystem core) : base(core)
    {
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        // if (!Core.SENSOR.IsGrounded)
        // {
        //     ChangeState(STATE.IN_AIR);
        // }
    }
}

public abstract class IdleState : GroundedState
{
    protected IdleState(CoreSystem core) : base(core)
    {
    }

    public override STATE Id => STATE.IDLE;

    public override void Enter()
    {
        base.Enter();
        Core.DISPLAY.ChangeAnim(CONSTANTS.IDLE_ANIM_NAME);
        Core.MOVEMENT.SetVelocity(Vector3.zero);
    }

    public override void Update()
    {
        base.Update();
        if (Core.NAVIGATION.MoveDirection.sqrMagnitude > .01f)
        {
            ChangeState(STATE.MOVE);
        }
    }

    public override void FixedUpdate()
    {
        Core.MOVEMENT.SetVelocity(Vector3.zero);
    }
}

public abstract class MoveState : GroundedState
{
    protected MoveState(CoreSystem core) : base(core)
    {
    }

    public override STATE Id => STATE.MOVE;

    public override void Enter()
    {
        base.Enter();
        Core.DISPLAY.ChangeAnim(CONSTANTS.RUN_ANIM_NAME);
    }

    public override void Update()
    {
        if (Core.NAVIGATION.MoveDirection.sqrMagnitude < .01f)
        {
            ChangeState(STATE.IDLE);
        }
    }

    public override void FixedUpdate()
    {
        if (Core.NAVIGATION.MoveDirection.sqrMagnitude < .01f)
            return;
    }
}

public abstract class InAirState : BaseLogicState
{
    protected InAirState(CoreSystem core) : base(core)
    {
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        if (Core.SENSOR.IsGrounded)
        {
            if (Core.NAVIGATION.MoveDirection.sqrMagnitude > 0.01f)
                ChangeState(STATE.MOVE);
            else
                ChangeState(STATE.IDLE);
        }
    }

    public override void FixedUpdate()
    {
        Core.MOVEMENT.ApplyGravity(1);
    }
}

public abstract class DeadState : BaseLogicState
{
    public override STATE Id => STATE.DEAD;
    protected DeadState(CoreSystem core) : base(core)
    {
    }

    public override void Enter()
    {
        Core.DISPLAY.ChangeAnim(CONSTANTS.IDLE_ANIM_NAME);
    }
}
#endregion

