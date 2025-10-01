using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin Data", menuName = "ScriptableObject/Item/Skin")]
public class Skin : Item
{
    public int model;
    public EQUIP equipType;
}