using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Enemy : Character
{
    public override int WeaponId { get => base.WeaponId; protected set => base.WeaponId = value; }

    public override void OnInit(CharacterStats stats = null)
    {
        base.OnInit(stats);
        Core.DISPLAY.TurnIndicator(false);
        WeaponId = Random.Range(0, DataManager.Ins.Get<EquipmentData>().weapons.Count);

        StartNavigation(false);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        StartNavigation(false);
    }

    public void SetUp(int initLevel)
    {
        Stats.Level.Set(initLevel);
        UpdateParamByLevel();
    }
}
