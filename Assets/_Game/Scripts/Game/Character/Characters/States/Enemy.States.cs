
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

