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
    public bool IsDead => Stats.HP.Value <= 0;
    public virtual int WeaponId { get; protected set; }
    float range;
    public float Range
    {
        get => range;
        set
        {
            range = value;
            Core.DISPLAY.UpdateIndicator(Range);
        }
    }

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

        Stats.Level.Reset();
        core.Initialize(Stats);

        UpdateParamByLevel();
    }

    public virtual void Run()
    {
        StartNavigation(true);
        Core.Run();
    }

    public virtual void OnDespawn()
    {
        Core.DISPLAY.Equipment.OnDespawn();
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
        core.OnDeath();
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
        //level up
        int add = LevelToAdd(targetLevel);
        Stats.Level.Plus(add);

        UpdateParamByLevel();
    }

    public virtual void UpdateParamByLevel()
    {
        float mult = LevelToMultiplier(Stats.Level.Value);

        //change atk range & scale
        Stats.AtkRange.Set(Stats.AtkRange.BaseValue * mult);
        Range = Stats.AtkRange.Value;
        Core.DISPLAY.SetSkinScale(mult);
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

    protected int LevelToAdd(float level)
    {
        int result = level switch
        {
            >= 1 and < 3 => 1,
            >= 3 and < 8 => 2,
            >= 8 and < 12 => 3,
            >= 12 and < 20 => 4,
            >= 20 => 5,
            _ => 0 // default
        };

        return result;
    }

    #endregion

    #region BOOSTER

    [HideInInspector]
    public bool IsBoost = false;

    public void BoosterUp()
    {
        if (!IsBoost)
        {
            IsBoost = true;
            Range = Stats.AtkRange.Value * 1.8f;
        }
    }

    public void BoosterOff()
    {
        IsBoost = false;
        Range = Stats.AtkRange.Value;
    }

    #endregion
}
