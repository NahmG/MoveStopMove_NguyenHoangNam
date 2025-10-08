using System;
using System.Collections.Generic;
using UnityEngine;

using URandom = UnityEngine.Random;

public class EnemySpawn : Singleton<EnemySpawn>
{
    public Action<Enemy> _OnEnemySpawn;
    public Action<Enemy> _OnEnemyDespawn;

    [SerializeField]
    int maxEnemy;
    [SerializeField]
    int maxEnemyOnField;

    public List<Enemy> Enemies { get; private set; } = new();
    int availableEnemy;

    int _enemyCount;
    public int CurrentEnemyCount => _enemyCount;

    #region SET_UP

    public void OnInit()
    {
        availableEnemy = maxEnemy;
        _enemyCount = maxEnemy;

        //spawn initial enemies
        for (int i = 0; i < maxEnemyOnField; i++)
        {
            Spawn(out _);
        }
    }

    public void Run()
    {
        Enemies.ForEach(x => x.Run());
    }

    public void OnDespawn()
    {
        DespawnAllEnemy();
        _enemyCount = 0;
        availableEnemy = 0;
    }
    #endregion 

    #region SPAWN_FUNC
    void Spawn(out Enemy e)
    {
        e = HBPool.Spawn<Enemy>(PoolType.ENEMY, GetSpawnPos(), Quaternion.identity);
        e.OnInit();

        Enemies.Add(e);
        availableEnemy--;

        _OnEnemySpawn?.Invoke(e);
    }

    void Despawn(Enemy enemy)
    {
        enemy.OnDespawn();
        HBPool.Despawn(enemy);
        _enemyCount--;

        if (Enemies.Contains(enemy))
            Enemies.Remove(enemy);

        _OnEnemyDespawn?.Invoke(enemy);
    }

    void DespawnAllEnemy()
    {
        foreach (var enemy in Enemies)
        {
            enemy.OnDespawn();
            HBPool.Despawn(enemy);
        }
        Enemies.Clear();
        availableEnemy = 0;
    }

    public void OnEnemyDespawn(Enemy enemy)
    {
        //Despawn old 
        Despawn(enemy);

        //Spawn new one
        if (availableEnemy > 0)
        {
            Spawn(out Enemy e);
            e.SetUp(GetEnemyInitLevel());
            e.Run();
        }
        if (_enemyCount <= 0)
        {
            GameplayManager.Ins.Player.ChangeState(STATE.WIN);
        }
    }

    int GetEnemyInitLevel()
    {
        float playerLvl = GameplayManager.Ins.Player.Stats.Level.Value;
        int rnd;
        if (playerLvl >= 2)
            rnd = (int)URandom.Range(1, playerLvl / 2);
        else rnd = 1;
        return rnd;
    }

    public Vector3 GetSpawnPos()
    {
        Vector3 spawnPos;
        int attempts = 0;
        do
        {
            attempts++;

            spawnPos = LevelManager.Ins.RandomPoint();
            if (IsValidPos(spawnPos))
                return spawnPos;

        } while (attempts < 1000);

        Debug.LogWarning("No valid spawn position found!");
        return spawnPos;
    }

    bool IsValidPos(Vector3 pos)
    {
        List<Character> chars = new(Enemies)
            {
                GameplayManager.Ins.Player
            };
        foreach (var _char in chars)
        {
            float dist = Vector3.Distance(pos, _char.TF.position);
            if (dist <= _char.Stats.AtkRange.BaseValue * 2)
                return false;
        }
        return true;
    }

    #endregion
}
