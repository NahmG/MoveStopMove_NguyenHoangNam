
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopData", menuName = "ScriptableObject/Data/ShopData", order = 0)]
public class ShopData : DataComponent
{
    [ListDrawerSettings(ListElementLabelName = "shop", ShowIndexLabels = true)]
    public List<ItemList> allItems = new();

    public List<T> GetItems<T>(SHOP type) where T : Item
    {
        return allItems[(int)type].items.ConvertAll(x => (T)x);
    }

    public T GetItem<T>(SHOP type, int index) where T : Item
    {
        return allItems[(int)type].items[index] as T;
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
                items[j].isEquip = false;
            }
        }

        Weapon first = GetItem<Weapon>(SHOP.WEAPON, 0);
        first.isEquip = true;
        first.isLock = false;
    }
}

[Serializable]
public class ItemList
{
    public SHOP shop;
    public List<Item> items = new();
}

public enum SHOP
{
    HAT = 0,
    PANT = 1,
    SHIELD = 2,
    SET_SKIN = 3,
    WEAPON = 4,
}