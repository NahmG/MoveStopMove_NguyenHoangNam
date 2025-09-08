using UnityEngine;

using Core;
using System.Collections;
using System.Collections.Generic;
public class Character : GameUnit, ICharacter
{
    #region CORE
    [SerializeField]
    protected CharacterStats Stats;
    [SerializeField]
    CoreSystem core;
    public CoreSystem Core => core;
    [SerializeField]
    COLOR defaultColor = COLOR.NONE;

    public bool IsDie => Stats.HP.Value <= 0;
    COLOR _color = COLOR.NONE;
    public COLOR Color
    {
        get => _color;
        set
        {
            _color = value;
            core.DISPLAY.ChangeColor(value);
        }
    }

    public virtual void OnInit(CharacterStats stats = null)
    {
        if (stats == null)
            Stats.Reset();
        else
            Stats = stats;

        Color = defaultColor;
        core.Initialize(Stats);
    }

    public virtual void OnDespawn()
    {
    }

    public virtual void OnDeath()
    {
        core.OnDeath();
        LastBrickCount = brickStack.Count;
        ClearBricks();
    }

    protected virtual void Update()
    {
        core.UpdateData();
    }

    protected virtual void FixedUpdate()
    {
        core.FixedUpdate();
    }

    public void StartNavigation(bool state)
    {
        if (state)
        {
            Core.NAVIGATION.StartNavigation();
        }
        else
        {
            Core.NAVIGATION.StopNavigation();
        }
    }

    #endregion

    #region BRICK
    [SerializeField]
    Transform brickHoldPoint;
    Stack<Brick> brickStack = new();
    public int BrickCount => brickStack.Count;
    public int LastBrickCount { get; private set; }

    public void AddBrick()
    {
        Vector3 pos = brickHoldPoint.position + .2f * brickStack.Count * Vector3.up;
        BrickOnChar brick = HBPool.Spawn<BrickOnChar>(PoolType.BRICK, pos, brickHoldPoint.rotation);

        brick.OnInit();
        brick.ChangeColor(Color);
        brick.TF.SetParent(brickHoldPoint);

        brickStack.Push(brick);
    }

    public void RemoveBrick()
    {
        if (brickStack.Count == 0) return;

        Brick brick = brickStack.Pop();
        brick.OnDespawn();
        HBPool.Despawn(brick);
    }

    public void ClearBricks()
    {
        while (brickStack.Count > 0)
            RemoveBrick();
    }

    #endregion

    #region PLATFORM
    protected List<BrickOnFloor> targetBricks = new();

    public void SetTargetBrick(List<BrickOnFloor> bricks)
    {
        targetBricks = bricks;
    }

    #endregion
}
