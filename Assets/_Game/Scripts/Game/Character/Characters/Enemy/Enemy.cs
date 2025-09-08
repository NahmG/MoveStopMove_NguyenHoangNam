using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Enemy : Character
{
    public override void OnInit(CharacterStats stats = null)
    {
        StartNavigation(true);
        base.OnInit(stats);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        StartNavigation(false);
    }
}
