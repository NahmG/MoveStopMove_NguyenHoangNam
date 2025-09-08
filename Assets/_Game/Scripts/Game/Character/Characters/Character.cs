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
}
