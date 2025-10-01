using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class UIShopWeapon : UICanvas
{
    [FoldoutGroup("Text")]
    [SerializeField]
    TMP_Text weaponName, lockText, cost, description;

    [FoldoutGroup("Buttons")]
    [SerializeField]
    UIButton closeBtn, leftBtn, rightBtn, buyBtn, equipBtn, adsBtn;

    [SerializeField]
    WeaponDisplayComponent display;

    Item currentWeapon;
    PlayerData playerData;
    PlayerEquipment playerEquip;

    void Awake()
    {
        closeBtn._OnClick += OnCloseBtnClick;
        leftBtn._OnClick += OnLeftRightBtnClick;
        rightBtn._OnClick += OnLeftRightBtnClick;
        buyBtn._OnClick += OnBuyBtnClick;
        equipBtn._OnClick += OnEquipBtnClick;

        display._OnWeaponSelect += OnWeaponSelect;
        display.Init();

        playerData ??= DataManager.Ins.Get<PlayerData>();
        playerEquip ??= GameplayManager.Ins.Player.Core.DISPLAY.Equipment as PlayerEquipment;
    }

    void OnDestroy()
    {
        closeBtn._OnClick -= OnCloseBtnClick;
        leftBtn._OnClick -= OnLeftRightBtnClick;
        rightBtn._OnClick -= OnLeftRightBtnClick;
        buyBtn._OnClick -= OnBuyBtnClick;
        equipBtn._OnClick -= OnEquipBtnClick;

        display._OnWeaponSelect -= OnWeaponSelect;
    }

    public override void Open(object param = null)
    {
        base.Open(param);
        display.ShowWeapon(0);
    }

    void OnCloseBtnClick(int index)
    {
        Hide();
        UIManager.Ins.OpenUI<UIMainMenu>();
    }

    void OnLeftRightBtnClick(int index)
    {
        switch (index)
        {
            case 0:
                display.ShowPreviousWeapon();
                break;
            case 1:
                display.ShowNextWeapon();
                break;
        }
    }

    void OnBuyBtnClick(int index)
    {
        if (currentWeapon == null) return;
        if (playerData.gold.Value < currentWeapon.cost) return;

        playerData.gold.Plus(currentWeapon.cost);
        currentWeapon.isLock = false;

        UpdateBtnState(currentWeapon);
    }

    void OnEquipBtnClick(int index)
    {
        if (currentWeapon == null) return;
        if (currentWeapon.isLock) return;

        equipBtn.SetState(UIButton.STATE.SELECTING);
        // Equip item
        playerEquip.EquipItem(currentWeapon);
    }

    void OnWatchAdsBtnClick(int index)
    {

    }

    void OnWeaponSelect(Item item)
    {
        currentWeapon = item;
        UpdateBtnState(item);

        if (item.isLock)
            cost.text = item.cost.ToString();

        description.text = item.description;
        weaponName.text = item.equipName;
    }

    void UpdateBtnState(Item item)
    {
        buyBtn.gameObject.SetActive(item.isLock);
        equipBtn.gameObject.SetActive(!item.isLock);
        lockText.gameObject.SetActive(item.isLock);

        if (!item.isLock)
        {
            if (playerEquip.IsEquip(item))
                equipBtn.SetState(UIButton.STATE.SELECTING);
            else
                equipBtn.SetState(UIButton.STATE.OPENING);
        }
    }
}