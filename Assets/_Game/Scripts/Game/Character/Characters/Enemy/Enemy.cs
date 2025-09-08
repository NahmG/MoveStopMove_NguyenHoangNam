using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Enemy : Character
{
    public Func<Character, Platform, Vector3> _OnFindNearestBridge;
    public Func<Character, Vector3> _OnFindNearestDoor;
    public Func<Character, Vector3> _OnFindFinishLine;

    public bool IsEnoughBrick
    {
        get
        {
            int rnd = Random.Range(10, 15);
            return BrickCount >= rnd;
        }
    }
    public bool IsEmptyBrick => BrickCount <= 0;
    Platform currentPlatform;

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

    public Vector3 FindNearestBrick()
    {
        List<BrickOnFloor> bricks = targetBricks;

        int index = 0;
        float minDist = float.MaxValue;

        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].IsTaken) continue;

            float dist = Vector3.Distance(TF.position, bricks[i].TF.position);
            if (dist < minDist)
            {
                minDist = dist;
                index = i;
            }
        }

        return bricks[index].TF.position;
    }

    public Vector3 FindNearestBridge()
    {
        return _OnFindNearestBridge.Invoke(this, currentPlatform);
    }

    public Vector3 FindNearestDoor()
    {
        return _OnFindNearestDoor.Invoke(this);
    }

    public Vector3 FindFinishLine()
    {
        return _OnFindFinishLine.Invoke(this);
    }

    public void SetPlatform(Platform platform)
    {
        currentPlatform = platform;
    }
}
