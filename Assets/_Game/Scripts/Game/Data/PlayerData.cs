using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/Data/Player Data")]
public class PlayerData : DataComponent
{
    public string playerName;
    public Stat gold;
    public bool isSet;
    public int[] equipIds = new int[] { -1, -1, -1, -1, -1, -1 };
    public int[] itemIds = new[] { -1, -1, -1, -1, 0 };

    public void SetData(int[] equipIds, int[] itemIds, float gold)
    {
        this.equipIds = equipIds;
        this.itemIds = itemIds;
        this.gold.Set(gold);
    }

    public void Clear()
    {
        playerName = "";
        gold.Set(0);
        isSet = false;
        equipIds = new int[] { -1, -1, -1, -1, -1, -1 };
        itemIds = new[] { -1, -1, -1, -1, 0 };
    }
}