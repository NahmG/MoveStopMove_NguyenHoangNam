using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

using URandom = UnityEngine.Random;

[CreateAssetMenu(fileName = "EquipmentData", menuName = "ScriptableObject/Data/Equipment Data")]
public class EquipmentData : DataComponent
{
    [ListDrawerSettings(ShowIndexLabels = true)] public List<Material> bodys;
    [ListDrawerSettings(ShowIndexLabels = true)] public List<GameObject> hats;
    [ListDrawerSettings(ShowIndexLabels = true)] public List<Material> pants;
    [ListDrawerSettings(ShowIndexLabels = true)] public List<GameObject> left_hands;
    [ListDrawerSettings(ShowIndexLabels = true)] public List<GameObject> tails;
    [ListDrawerSettings(ShowIndexLabels = true)] public List<GameObject> wings;
    [ListDrawerSettings(ShowIndexLabels = true)] public List<GameObject> weapons;

    List<IList> allItems = new();

    public override void Init()
    {
        allItems.Add(bodys);
        allItems.Add(hats);
        allItems.Add(pants);
        allItems.Add(left_hands);
        allItems.Add(tails);
        allItems.Add(wings);
        allItems.Add(weapons);
    }

    public object GetItem(EQUIP type, int id)
    {
        return allItems[(int)type][id];
    }

    public IList GetItemList(EQUIP type)
    {
        return allItems[(int)type];
    }

    public object GetRandomItem(EQUIP type)
    {
        IList list = allItems[(int)type];
        int id = URandom.Range(0, list.Count);

        return list[id];
    }

    public object GetRandomItemExclude(EQUIP type, int excludeId)
    {
        if (excludeId < 0)
            return GetRandomItem(type);

        IList list = allItems[(int)type];
        int id = URandom.Range(0, excludeId);

        return list[id];
    }

    public int[] GetRandomSet()
    {
        int[] set = new int[6];
        set[0] = URandom.Range(0, bodys.Count);
        set[1] = URandom.Range(0, hats.Count);
        set[2] = URandom.Range(0, pants.Count);
        set[3] = URandom.Range(0, left_hands.Count);
        set[4] = URandom.Range(0, tails.Count);
        set[5] = URandom.Range(0, wings.Count);

        return set;
    }
}