using System.Collections.Generic;
using UnityEngine;

public class EquipController : MonoBehaviour
{
    Dictionary<EQUIP_TYPE, EquipComponent> compDict = new();

}

public enum EQUIP_TYPE
{
    BODY,
    HAT,
    PANT,
    RIGHT_HAND,
    LEFT_HAND,
    TAIL,
    WING,
}