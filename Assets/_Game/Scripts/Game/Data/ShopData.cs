
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopData", menuName = "ScriptableObject/Data/ShopData", order = 0)]
public class ShopData : DataComponent
{
    [ListDrawerSettings(ListElementLabelName = "shop", ShowIndexLabels = true)]
    public List<ItemList> allItems = new();

    public List<Item> GetItems(SHOP type)
    {
        return allItems[(int)type].items.ToList();
    }

    public Item GetItem(SHOP type, int index)
    {
        return allItems[(int)type].items[index];
    }

    public void SetData(List<SaveData.IntList> boughtItems)
    {
        for (int i = 0; i < boughtItems.Count; i++)
        {
            List<Item> items = allItems[i].items;
            for (int j = 0; j < boughtItems[i].items.Count; j++)
            {
                int itemIndex = boughtItems[i].items[j];
                if (itemIndex >= 0 && itemIndex < items.Count)
                    items[itemIndex].isLock = false;
            }
        }
    }
    public void Clear()
    {
        for (int i = 0; i < allItems.Count; i++)
        {
            List<Item> items = allItems[i].items;
            for (int j = 0; j < items.Count; j++)
            {
                items[j].isLock = true;
            }
        }
    }
}

[Serializable]
public class ItemList
{
    public SHOP shop;
    public List<Item> items = new();
}

