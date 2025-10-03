using System;
using UnityEngine;

[Serializable]
public class BoosterController
{
    [SerializeField]
    Booster pref;
    Booster current;

    public void Spawn()
    {
        Despawn();
        Vector3 spawnPos = EnemySpawn.Ins.GetSpawnPos();

        current = GameObject.Instantiate(pref, spawnPos, Quaternion.identity);
        current.Init(this);
    }

    public void Despawn()
    {
        if (current != null)
        {
            GameObject.Destroy(current.gameObject);
        }
    }
}