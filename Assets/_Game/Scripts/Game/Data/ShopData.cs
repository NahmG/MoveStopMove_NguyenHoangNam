
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopData", menuName = "ScriptableObject/Data/ShopData", order = 0)]
public class ShopData : DataComponent
{
    [ListDrawerSettings(ListElementLabelName = "shop", ShowIndexLabels = true)]
    public List<ItemList> allItems = new();

    public void SetData(List<ItemList> allItems)
    {
        this.allItems = allItems;
    }

    public List<T> GetItems<T>(int index) where T : Item
    {
        return allItems[index].items.ConvertAll(x => (T)x);
    }

    public Item GetItem(int shopIndex, int itemIndex)
    {
        return allItems[shopIndex].items[itemIndex];
    }

    [Serializable]
    public class ItemList
    {
        public SHOP shop;
        public List<Item> items = new();
    }
}

public enum SHOP
{
    HAT = 0,
    PANT = 1,
    SHIELD = 2,
    SET_SKIN = 3,
    WEAPON = 4,
}