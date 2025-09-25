using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/Data/Player Data")]
public class PlayerData : DataComponent
{
    public Stat gold;
    public bool isSet;
    public int[] skinIds = new int[] { -1, -1, -1, -1, -1, -1 };
    public int weaponId = -1;

    public void SetData(int[] skinIds, int weaponId, float gold)
    {
        this.skinIds = skinIds;
        this.weaponId = weaponId;
        this.gold.Set(gold);
    }
}