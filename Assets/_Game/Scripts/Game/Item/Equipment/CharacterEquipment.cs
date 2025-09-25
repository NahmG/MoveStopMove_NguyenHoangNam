using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;

public class CharacterEquipment : SerializedMonoBehaviour
{
    [OdinSerialize]
    protected EquipComponent[] equipPart;

    protected int[] currentSet;
    protected int currentWeapon;

    public virtual void Initialize()
    {
        equipPart.ForEach(x => x.Initialize());
    }

    public virtual void Equip(EQUIP Id, int index)
    {
        equipPart[(int)Id].Equip(index);
        if (Id == EQUIP.WEAPON)
            currentWeapon = index;
        else
            currentSet[(int)Id] = index;
    }

    public virtual void Equip(int[] set)
    {
        for (int i = 0; i < set.Length; i++)
        {
            equipPart[i].Equip(set[i]);
        }
        currentSet = set;
    }

    public virtual void Equip(Item item)
    {
        if (item == null) return;
        if (item is Skin skin)
        {
            if (skin.isSet)
                Equip(skin.setId);
            else
                Equip(skin.type, skin.modelId);
        }
        else if (item is Weapon weapon)
        {
            Equip(EQUIP.WEAPON, weapon.modelId);
        }
    }

    public virtual void UnEquip(EQUIP Id)
    {
        equipPart[(int)Id].UnEquip();
        if (Id == EQUIP.WEAPON)
            currentWeapon = -1;
        else
            currentSet[(int)Id] = -1;
    }

    public virtual void UnEquip(int[] set)
    {
        for (int i = 0; i < set.Length; i++)
        {
            equipPart[i].UnEquip();
        }
        currentSet = new int[] { -1, -1, -1, -1, -1, -1 };
    }

    public virtual void UnEquip(Item item)
    {
        if (item == null) return;
        if (item is Skin skin)
        {
            if (skin.isSet)
                UnEquip(skin.setId);
            else
                UnEquip(skin.type);
        }
        else if (item is Weapon)
        {
            UnEquip(EQUIP.WEAPON);
        }
    }
}

public enum EQUIP
{
    NONE = -1,
    BODY = 0,
    HAT = 1,
    PANT = 2,
    LEFT_HAND = 3,
    TAIL = 4,
    WING = 5,
    WEAPON = 6,
}