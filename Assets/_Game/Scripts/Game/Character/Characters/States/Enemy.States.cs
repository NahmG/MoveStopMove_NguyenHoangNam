
using Core;
using Core.Navigation;
using UnityEngine;

public class EnemyIdleState : IdleState
{
    EnemyNavigation _nav;
    public EnemyIdleState(CoreSystem core) : base(core)
    {
        _nav = (EnemyNavigation)core.NAVIGATION;
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }
}

public class EnemyFindBrickState : MoveState
{
    override public STATE Id => STATE.FIND_BRICK;
    Enemy _enemy;
    EnemyNavigation _nav;
    public EnemyFindBrickState(CoreSystem core) : base(core)
    {
        _enemy = (Enemy)core.CHARACTER;
        _nav = (EnemyNavigation)core.NAVIGATION;
    }

    public override void Enter()
    {
        base.Enter();

        _nav.FindNearestBrick();
        Core.MOVEMENT.SetDestination(_nav.Destination);
    }

    public override void Update()
    {
        if (_enemy.IsEnoughBrick)
            ChangeState(STATE.BUILD_BRIDGE);
        else if (_nav.ReachedDestination())
        {
            _nav.FindNearestBrick();
            Core.MOVEMENT.SetDestination(_nav.Destination);
        }
    }

    public override void Exit()
    {
    }
}

public class EnemyBuildBridgeState : MoveState
{
    override public STATE Id => STATE.BUILD_BRIDGE;

    Enemy _enemy;
    EnemyNavigation _nav;
    public EnemyBuildBridgeState(CoreSystem core) : base(core)
    {
        _enemy = (Enemy)core.CHARACTER;
        _nav = (EnemyNavigation)core.NAVIGATION;
    }

    public override void Enter()
    {
        base.Enter();

        _nav.FindNearestBridge();
        Core.MOVEMENT.SetDestination(_nav.Destination);
    }

    public override void Update()
    {
        if (_nav.ReachedDestination())
            ChangeState(STATE.FIND_DOOR);
    }

    public override void Exit()
    {
    }
}

public class EnemyFindDoorState : MoveState
{
    override public STATE Id => STATE.FIND_DOOR;
    Enemy _enemy;
    EnemyNavigation _nav;
    public EnemyFindDoorState(CoreSystem core) : base(core)
    {
        _enemy = (Enemy)core.CHARACTER;
        _nav = (EnemyNavigation)core.NAVIGATION;
    }

    public override void Enter()
    {
        base.Enter();

        _nav.FindNearestDoor();
        Core.MOVEMENT.SetDestination(_nav.Destination);
    }

    public override void Update()
    {
        if (_nav.ReachedDestination())
        {
            ChangeState(STATE.FIND_BRICK);
        }
    }


    public override void Exit()
    {
    }
}

public class EnemyGoToFinishLineState : MoveState
{
    override public STATE Id => STATE.GO_TO_FINISH_LINE;
    Enemy _enemy;
    EnemyNavigation _nav;
    public EnemyGoToFinishLineState(CoreSystem core) : base(core)
    {
        _enemy = (Enemy)core.CHARACTER;
        _nav = (EnemyNavigation)core.NAVIGATION;
    }

    public override void Enter()
    {
        base.Enter();

        _nav.FindFinishLine();
        Core.MOVEMENT.SetDestination(_nav.Destination);
    }

    public override void Update()
    {
    }


    public override void Exit()
    {
    }

}
public class EnemyDeadState : DeadState
{
    public EnemyDeadState(CoreSystem core) : base(core)
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