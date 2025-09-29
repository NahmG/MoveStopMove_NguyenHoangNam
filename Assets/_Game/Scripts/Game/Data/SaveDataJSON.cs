using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

public class SaveDataJSON : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] ShopData shopData;
    SaveData saveData;

    [Button]
    public void SaveData()
    {
        saveData = new SaveData(playerData, shopData);
        string json = JsonUtility.ToJson(saveData);
        Debug.Log(json);

        using StreamWriter writer = new(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json");
        writer.Write(json);
    }

    [Button]
    public void LoadData()
    {
        string json = string.Empty;

        using (StreamReader reader = new(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json"))
        {
            json = reader.ReadToEnd();
        }

        SaveData data = JsonUtility.FromJson<SaveData>(json);
        playerData.SetData(data.equipIds, data.itemIds, data.gold);
        shopData.SetData(data.boughtItems);
    }
}

[Serializable]
public class SaveData
{
    public string playerName;
    public float gold;
    public int[] equipIds;
    public int[] itemIds;

    [Serializable]
    public class IntList
    {
        public List<int> items = new();
        public IntList(List<int> items)
        {
            this.items = items;
        }
    }
    public List<IntList> boughtItems;

    public SaveData(PlayerData playerData, ShopData shopData)
    {
        playerName = playerData.playerName;
        gold = playerData.gold.Value;
        equipIds = playerData.equipIds;
        itemIds = playerData.itemIds;

        boughtItems = new();
        GetBoughtItems(shopData);
    }

    void GetBoughtItems(ShopData shopData)
    {
        for (int i = 0; i < shopData.allItems.Count; i++)
        {
            List<int> items = new();
            for (int j = 0; j < shopData.allItems[i].items.Count; j++)
            {
                Item item = shopData.allItems[i].items[j];

                if (!item.isLock)
                    items.Add(j);
            }

            boughtItems.Add(new IntList(items));
        }
    }
}

