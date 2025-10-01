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

    protected int[] equipId = new int[6] { -1, -1, -1, -1, -1, -1 };
    protected bool isSet;

    public Color MainColor { get; private set; }

    public virtual void Initialize()
    {
        equipPart.ForEach(x => x.Initialize());
        MainColor = equipPart[0] is RenderComponent rend ? rend.defaultMat.color : Color.black;
    }

    public virtual void OnDespawn() { }

    public virtual void Equip(EQUIP Id, int index)
    {
        equipPart[(int)Id].Equip(index);

        if (Id != EQUIP.WEAPON)
            equipId[(int)Id] = index;
    }

    public virtual void Equip(int[] set)
    {
        for (int i = 0; i < set.Length; i++)
        {
            equipPart[i].Equip(set[i]);
            equipId[i] = set[i];
        }
        isSet = true;
    }

    public virtual void UnEquip(EQUIP Id)
    {
        equipPart[(int)Id].UnEquip();
        equipId[(int)Id] = -1;
    }

    public virtual void UnEquip()
    {
        for (int i = 0; i < equipPart.Length; i++)
        {
            equipPart[i].UnEquip();
        }

        isSet = false;
        equipId = new int[] { -1, -1, -1, -1, -1, -1 };
    }
}

