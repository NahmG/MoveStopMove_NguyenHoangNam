using UnityEngine;

using Core;
using System.Collections;
using System.Collections.Generic;
public class Character : GameUnit, ICharacter
{
    #region CORE
    public CharacterStats Stats;
    [SerializeField]
    CoreSystem core;
    public CoreSystem Core => core;
    [SerializeField]
    COLOR defaultColor = COLOR.NONE;
    public bool IsDead => Stats.HP.Value <= 0;
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

    [HideInInspector]
    public bool IsUlti;

    protected virtual void Awake()
    {
        CharacterStats newStats = ScriptableObject.CreateInstance<CharacterStats>();
        newStats.OnInit(Stats);
        Stats = newStats;
    }

    public virtual void OnInit(CharacterStats stats = null)
    {
        if (stats == null)
            Stats.Reset();
        else
            Stats = stats;

        Color = defaultColor;
        Stats.Level.Reset();

        core.Initialize(Stats);
    }

    public virtual void Run()
    {
        StartNavigation(true);
        Core.Run();
    }

    public virtual void OnDespawn()
    {
    }

    public void OnHit(int dmg)
    {
        Stats.HP.Plus(-dmg);

        if (Stats.HP.Value <= 0)
        {
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {
        core.OnDespawn();
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

    public virtual void ChangeState(STATE state)
    {
        Core.StateMachine.ChangeState(state);
    }

    #endregion

    #region LEVEL

    /// <param name="targetLevel">Target's current level</param>
    public virtual void OnLevelUp(float targetLevel)
    {
        float mult = LevelToMultiplier(Stats.Level.Value);
        int add = LevelToAddend(targetLevel);

        //level up
        Stats.Level.Plus(add);

        //change atk range & scale
        Stats.AtkRange.Set(Stats.AtkRange.BaseValue * mult);
        Core.DISPLAY.Scale = mult;
    }

    protected float LevelToMultiplier(float level)
    {
        float result = level switch
        {
            >= 1 and < 3 => 1f,
            >= 3 and < 8 => 1.2f,
            >= 8 and < 12 => 1.5f,
            >= 12 and < 20 => 1.8f,
            >= 20 => 2,
            _ => 1f // default
        };

        return result;
    }

    protected int LevelToAddend(float level)
    {
        int result = level switch
        {
            >= 1 and < 3 => 1,
            >= 3 and < 8 => 2,
            >= 8 and < 12 => 3,
            >= 12 and < 20 => 4,
            >= 20 => 5,
            _ => 1 // default
        };

        return result;
    }

    #endregion
}
