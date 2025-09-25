using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "ScriptableObject/Item/Weapon")]
public class Weapon : Item
{
    public EQUIP equipType = EQUIP.WEAPON;
    public int modelId = -1;
}