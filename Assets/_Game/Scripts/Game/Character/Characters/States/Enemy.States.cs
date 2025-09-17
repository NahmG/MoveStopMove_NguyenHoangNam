
using Core;
using Core.Movement;
using Core.Navigation;
using UnityEngine;

public class EnemyIdleState : IdleState
{
    float idleTime;
    float timer;

    public EnemyIdleState(CoreSystem core) : base(core)
    {
    }

    public override void Enter()
    {
        base.Enter();
        idleTime = GetRandomTime(2, 3);
        timer = Time.time;
    }

    public override void Update()
    {
        if (Core.SENSOR.Target != null && !Core.ATTACK.IsAtkCooldown)
        {
            ChangeState(STATE.ATTACK);
        }

        if (Time.time >= timer + idleTime)
        {
            ChangeState(STATE.MOVE);
        }
    }
}

public class EnemyMoveState : MoveState
{
    EnemyNavigation _nav;
    EnemyMovement _move;
    public EnemyMoveState(CoreSystem core) : base(core)
    {
        _nav = (EnemyNavigation)Core.NAVIGATION;
        _move = (EnemyMovement)Core.MOVEMENT;
    }

    public override void Enter()
    {
        base.Enter();
        _nav.GetRandomDestination();
        _move.SetDestination(_nav.Destination);
    }

    public override void Update()
    {
        if (_nav.ReachDestination())
        {
            ChangeState(STATE.IDLE);
        }
    }
}

public class EnemyAttackState : AttackState
{
    Enemy _enemy;
    public EnemyAttackState(CoreSystem core) : base(core)
    {
        _enemy = (Enemy)Core.CHARACTER;
    }

    public override void Enter()
    {
        base.Enter();

    }

    protected override void RotateTowardTarget()
    {
        ICharacter target = Core.SENSOR.Target;
        Vector3 dir = target.TF.position - _char.TF.position;
        dir.y = 0;

        _enemy.TF.rotation = Quaternion.LookRotation(dir);
    }
}


public class EnemyDeadState : DeadState
{
    public EnemyDeadState(CoreSystem core) : base(core)
    {
    }
}

