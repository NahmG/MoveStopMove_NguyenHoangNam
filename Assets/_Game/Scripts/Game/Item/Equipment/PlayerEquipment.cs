using UnityEngine;

public class PlayerEquipment : CharacterEquipment
{
    PlayerData playerData;
    public PlayerData PlayerData
    {
        get
        {
            if (playerData == null)
                playerData = DataManager.Ins.Get<PlayerData>();
            return playerData;
        }
    }

    ShopData shopData;
    public ShopData ShopData
    {
        get
        {
            if (shopData == null)
                shopData = DataManager.Ins.Get<ShopData>();
            return shopData;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        Load();
    }

    public override void Equip(Item item)
    {
        if (item is Skin && PlayerData.isSet)
        {
            UnEquip(PlayerData.equipIds);
        }
        base.Equip(item);
    }

    public void UnEquipOldItem(SHOP shop)
    {
        Item oldItem = ShopData.GetItems<Item>(shop).Find(x => x.isEquip);
        if (oldItem == null) return;
        UnEquip(oldItem);
        oldItem.isEquip = false;
    }

    public void Preview(Item item)
    {

    }

    public void EquipItem(Item item)
    {

    }

    public void UnEquipItem(Item item)
    {

    }

    public void Save()
    {
        PlayerData.itemIds[(int)SHOP.WEAPON] = currentWeapon;
        PlayerData.equipIds = currentSet;
    }

    public void Load()
    {
        Equip(PlayerData.equipIds);
        Equip(EQUIP.WEAPON, PlayerData.itemIds[(int)SHOP.WEAPON]);
    }
}