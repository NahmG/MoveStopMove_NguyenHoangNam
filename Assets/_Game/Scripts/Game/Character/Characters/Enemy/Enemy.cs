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
        TurnOnIndicator(false);
        WeaponId = Random.Range(0, DataManager.Ins.Get<EquipmentData>().weapons.Count);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        EnemySpawn.Ins.OnEnemyDespawn(this);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        StartNavigation(false);
    }

    [SerializeField]
    GameObject indicator;

    public void TurnOnIndicator(bool isOn)
    {
        indicator.SetActive(isOn);
    }

    public void SetUp()
    {
        //TODO: set random skin or smthg'
    }
}
