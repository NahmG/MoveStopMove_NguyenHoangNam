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

    void Awake()
    {
        closeBtn._OnClick += OnCloseBtnClick;
        leftBtn._OnClick += OnLeftRightBtnClick;
        rightBtn._OnClick += OnLeftRightBtnClick;

        display._OnWeaponSelect += OnWeaponSelect;
        display.Init();
    }

    void OnDestroy()
    {
        closeBtn._OnClick -= OnCloseBtnClick;
        leftBtn._OnClick -= OnLeftRightBtnClick;
        rightBtn._OnClick -= OnLeftRightBtnClick;

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

    }

    void OnEquipBtnClick(int index)
    {

    }

    void OnWatchAdsBtnClick(int index)
    {

    }

    void OnWeaponSelect(WeaponData data)
    {
        buyBtn.gameObject.SetActive(data.isLock);
        equipBtn.gameObject.SetActive(!data.isLock);
        lockText.gameObject.SetActive(data.isLock);

        if (data.isLock)
        {
            cost.text = data.cost.ToString();
        }

        description.text = data.description;
        weaponName.text = data.name;
    }
}