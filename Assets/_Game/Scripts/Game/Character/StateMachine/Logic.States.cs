using UnityEngine;

#region BASE STATE

using Core;

public abstract class BaseLogicState : BaseState
{
    protected CoreSystem Core;
    protected CharacterStats Stats;

    protected bool isAnimFinish;
    protected float animTime;

    float timer;

    public BaseLogicState(CoreSystem core)
    {
        Core = core;
    }

    public override void Enter()
    {
        base.Enter();
        timer = Time.time;
        isAnimFinish = false;
    }

    public override void Update()
    {
        if (Time.time >= timer + animTime)
        {
            isAnimFinish = true;
        }
    }

    protected float GetRandomTime(float min, float max)
    {
        return Random.Range(min, max);
    }
}

public abstract class GroundedState : BaseLogicState
{
    protected bool IsAttackCooldown => Core.ATTACK.IsAtkCooldown;
    protected Character Target => Core.SENSOR.Target;

    protected GroundedState(CoreSystem core) : base(core)
    {
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
        base.Update();
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

    protected AttackState(CoreSystem core) : base(core)
    {
        _char = core.CHARACTER;
        animTime = Core.DISPLAY.AtkDuration;
    }

    public override void Enter()
    {
        base.Enter();
        Core.MOVEMENT.StopMovement();
        RotateTowardTarget();

        Core.ATTACK.IsAttack = true;

        if (_char.IsUlti)
        {
            Core.DISPLAY.ChangeAnim(CONSTANTS.ULTI_ANIM_NAME);
        }
        Core.DISPLAY.ChangeAnim(CONSTANTS.ATTACK_ANIM_NAME);
    }

    public override void Update()
    {
        base.Update();
        if (isAnimFinish)
        {
            ChangeState(STATE.IDLE);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Core.ATTACK.IsAttack = false;
    }

    protected virtual void RotateTowardTarget() { }
}

public abstract class DeadState : BaseLogicState
{
    public override STATE Id => STATE.DEAD;
    Character _char;

    protected DeadState(CoreSystem core) : base(core)
    {
        _char = core.CHARACTER;
        animTime = Core.DISPLAY.DeadDuration;
    }

    public override void Enter()
    {
        base.Enter();
        Core.DISPLAY.ChangeAnim(CONSTANTS.DEAD_ANIM_NAME);
        Core.MOVEMENT.StopMovement();
    }

    public override void Update()
    {
        base.Update();
        if (isAnimFinish)
        {
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {
        _char.OnDespawn();
        if (_char is Enemy e)
        {
            EnemySpawn.Ins.OnEnemyDespawn(e);
        }
    }
}
#endregion

