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

    protected float GetRandomTime(float min, float max)
    {
        return Random.Range(min, max);
    }
}

public abstract class IdleState : GroundedState
{
    protected Character _char;

    protected IdleState(CoreSystem core) : base(core)
    {
        _char = Core.CHARACTER;
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

        else if (Core.SENSOR.Target != null && !Core.ATTACK.IsAtkCooldown)
        {
            ChangeState(STATE.ATTACK);
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
}

public abstract class InAirState : BaseLogicState
{
    public override STATE Id => STATE.IN_AIR;
    protected InAirState(CoreSystem core) : base(core)
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

public abstract class AttackState : BaseLogicState
{
    public override STATE Id => STATE.ATTACK;
    protected Character _char;

    protected float timer;

    protected AttackState(CoreSystem core) : base(core)
    {
        _char = core.CHARACTER;
    }

    public override void Enter()
    {
        base.Enter();
        timer = Time.time;
        RotateTowardTarget();

        if (_char.IsUlti)
        {
            Core.DISPLAY.ChangeAnim(CONSTANTS.ULTI_ANIM_NAME);
        }
        Core.DISPLAY.ChangeAnim(CONSTANTS.ATTACK_ANIM_NAME);
    }

    public override void Update()
    {
        if (Core.NAVIGATION.MoveDirection.sqrMagnitude > .01f)
        {
            ChangeState(STATE.MOVE);
        }

        if (Core.SENSOR.Target == null)
        {
            ChangeState(STATE.IDLE);
        }

        if (Time.time >= timer + Core.DISPLAY.AtkDuration)
        {
            ChangeState(STATE.IDLE);
        }
    }

    protected virtual void RotateTowardTarget() { }
}

public abstract class DeadState : BaseLogicState
{
    public override STATE Id => STATE.DEAD;
    Character _char;
    float timer;

    protected DeadState(CoreSystem core) : base(core)
    {
        _char = core.CHARACTER;
    }

    public override void Enter()
    {
        Core.DISPLAY.ChangeAnim(CONSTANTS.DEAD_ANIM_NAME);
        timer = Time.time;

        Core.MOVEMENT.StopMovement();
    }

    public override void Update()
    {
        if (Time.time >= timer + Core.DISPLAY.DeadDuration)
            _char.OnDespawn();
    }
}
#endregion

