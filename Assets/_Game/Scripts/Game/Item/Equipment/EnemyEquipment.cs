using UnityEngine;

public class EnemyEquipment : CharacterEquipment
{
    override public void Initialize()
    {
        base.Initialize();

        int[] randomSet = DataManager.Ins.Get<EquipmentData>().GetRandomSet();
        Equip(randomSet);
    }
}