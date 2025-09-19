using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField]
    List<EquipComponent> equipPart;

    public void Equip(EQUIP_TYPE Id, int index)
    {
        equipPart[(int)Id].Equip(index);
    }

    public void UnEquip(EQUIP_TYPE Id)
    {
        equipPart[(int)Id].UnEquip();
    }
}

public enum EQUIP_TYPE
{
    NONE = -1,
    BODY = 0,
    HAT = 1,
    PANT = 2,
    RIGHT_HAND = 3,
    LEFT_HAND = 4,
    TAIL = 5,
    WING = 6,
}