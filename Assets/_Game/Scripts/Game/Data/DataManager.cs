using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

using URandom = UnityEngine.Random;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] EquipmentData equipment;
    [SerializeField] ShopData shop;
    [SerializeField] PlayerData player;

    private Dictionary<Type, DataComponent> dataDict = new();

    void Awake()
    {
        Register(equipment);
        Register(shop);
        Register(player);

        foreach (var data in dataDict.Values)
        {
            data.Init();
        }
    }

    public void Register<T>(T data) where T : DataComponent
    {
        var type = typeof(T);
        if (dataDict.ContainsKey(type))
            dataDict[type] = data;
        else
            dataDict.Add(type, data);
    }

    public T Get<T>() where T : DataComponent
    {
        Type type = typeof(T);
        if (!dataDict.ContainsKey(type))
        {
            dataDict[type] = player;
        }
        return dataDict[type] as T;
    }

    [Button]
    public void ClearData()
    {
        player.Clear();
        shop.Clear();
    }
}

public abstract class DataComponent : SerializedScriptableObject
{
    public virtual void Init() { }
}
