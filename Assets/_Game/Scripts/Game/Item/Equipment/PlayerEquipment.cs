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

    public void AttachModel(Item item)
    {
        if (isSet)
            UnEquip();

        if (item is Skin skin)
            Equip(skin.equipType, skin.model);
        else if (item is FullSkin full)
            Equip(full.setId);
    }

    public void DetachModel(Item item)
    {
        if (item is Skin skin)
            UnEquip(skin.equipType);
        else
            UnEquip();
    }

    public void EquipItem(Item item)
    {
        if (item.shop == SHOP.SET_SKIN)
        {
            PlayerData.itemId[(int)SHOP.HAT] = -1;
            PlayerData.itemId[(int)SHOP.PANT] = -1;
            PlayerData.itemId[(int)SHOP.SHIELD] = -1;
        }
        else
        {
            PlayerData.itemId[(int)SHOP.SET_SKIN] = -1;
        }

        PlayerData.itemId[(int)item.shop] = item.Id;
        AttachModel(item);
    }

    public void UnEquipItem(Item item)
    {
        PlayerData.itemId[(int)item.shop] = -1;
        DetachModel(item);
    }

    public bool IsEquip(Item item)
    {
        return PlayerData.itemId[(int)item.shop] == item.Id;
    }

    public void Save()
    {
        PlayerData.equipId = equipId;
    }

    public void Load()
    {
        equipId = PlayerData.equipId;
        Equip(equipId);

        int weapon = PlayerData.itemId[(int)SHOP.WEAPON];
        Equip(EQUIP.WEAPON, weapon);
    }
}