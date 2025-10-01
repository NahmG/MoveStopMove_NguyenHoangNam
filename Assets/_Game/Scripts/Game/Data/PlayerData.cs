using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/Data/Player Data")]
public class PlayerData : DataComponent
{
    public string playerName;
    public Stat gold;
    public int[] equipId = new int[] { -1, -1, -1, -1, -1, -1 };
    public int[] itemId = new[] { -1, -1, -1, -1, 0 };

    public void SetData(int[] equipId, int[] itemId, float gold)
    {
        this.equipId = equipId;
        this.itemId = itemId;
        this.gold.Set(gold);
    }

    public void Clear()
    {
        playerName = "";
        gold.Set(0);
        equipId = new int[] { -1, -1, -1, -1, -1, -1 };
        itemId = new[] { -1, -1, -1, -1, 0 };
    }
}