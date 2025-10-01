using UnityEngine;

public class EnemyEquipment : CharacterEquipment
{
    override public void Initialize()
    {
        base.Initialize();

        equipId = DataManager.Ins.Get<EquipmentData>().GetRandomSet();
        Equip(equipId);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        UnEquip();
    }
}